using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;
using HonewellBot.Handlers;

namespace HonewellBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Luis.LuisTemplate res = await Luis.GetIntent.FetchTemplateAsync(activity.Text);
                string Intent = res.intents[0].intent;

                Activity reply;

                if (activity.Text.Contains("@email:"))
                {
                    string[] result = await GetFromEmail.get(activity.Text.Substring(7));
                    reply = activity.CreateReply(result[0]);
                    connector.Conversations.ReplyToActivity(reply);
                    reply = activity.CreateReply(result[1]);
                    connector.Conversations.ReplyToActivity(reply);
                    reply = activity.CreateReply(result[2]);
                }

                else if (Intent.Equals("FindFriend"))
                {
                    if (res.entities.Length > 0)
                    {
                        List<string> emails = await GetFromName.get(res.entities[0].entity);
                        reply = activity.CreateReply("Here are the users with the name : " + res.entities[0].entity);
                        reply.Recipient = activity.From;
                        reply.Type = "message";
                        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                        reply.Attachments = new List<Attachment>();

                        foreach (var x in emails)
                        {
                            List<CardImage> images = new List<CardImage>();
                            images.Add(new CardImage("http://nulm.gov.in/images/user.png"));

                            List<CardAction> actions = new List<CardAction>();
                            CardAction action = new CardAction()
                            {
                                Value = "@email:"+x,
                                Type = ActionTypes.PostBack,
                                Title = "FIND"
                            };
                            actions.Add(action);

                            HeroCard card = new HeroCard()
                            {
                                Images = images,
                                Buttons = actions,
                                Title = "Email",
                                Subtitle = x
                            };

                            Attachment a = card.ToAttachment();
                            reply.Attachments.Add(a);
                        }
                        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    }
                    else
                    {
                        reply = activity.CreateReply("Sorry, I did not understand that");
                    }
                }

                else
                {
                    reply = activity.CreateReply("Sorry, I did not understand that");
                }
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
                ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                connector.Conversations.ReplyToActivity(message.CreateReply("Hello, I am HoneyWell Bot, I can help you find you colleagues! ask me questions like \"Where is xyz\""));
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {

            }

            return null;
        }
    }
}
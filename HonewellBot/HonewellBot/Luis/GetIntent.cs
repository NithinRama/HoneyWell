using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace HonewellBot.Luis
{
    public class GetIntent
    {
        public static async Task<LuisTemplate> FetchTemplateAsync(string s)
        {
            string LUISapp = "https://api.projectoxford.ai/luis/v1/application?id=e4a7d459-7a76-4fac-979c-0247f056f8ce&subscription-key=9ba5a167ea0d4b2ca890c2a04786514f&q=";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(LUISapp + s);
            client.Dispose();
            LuisTemplate result = JsonConvert.DeserializeObject<LuisTemplate>(response);
            return result;
        }
    }
}
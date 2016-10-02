using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HonewellBot.Handlers
{
    public class GetFromName
    {
        public async static Task<List<string>> get(string name)
        {
            List<string> emails = new List<string>();
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(Constants.GetFromNameLink + name);
            EmailTemplate[] results = JsonConvert.DeserializeObject<EmailTemplate[]>(result);
            foreach(var x in results)
            {
                emails.Add(x.email);
            }
            return emails;
        }
    }
}
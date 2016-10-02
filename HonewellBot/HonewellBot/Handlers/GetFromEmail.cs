using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace HonewellBot.Handlers
{
    public class GetFromEmail
    {
        public async static Task<string[]> get(string email)
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(Constants.GetFromEmailLink + email);
            NameTemplate template = JsonConvert.DeserializeObject<NameTemplate>(result);
            string[] res = new string[3];
            res[0] = "User was last reported at :";
            if(template.laptop.Length > 0)
            res[1] = "1.Laptop at : " + template.laptop[0].location1.name;
            if(template.mobile.Length > 0)
            res[2] = "2.Mobile at : " + template.mobile[0].location1.name;
            return res;
        }
    }
}
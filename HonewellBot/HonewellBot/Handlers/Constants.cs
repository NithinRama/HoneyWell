using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonewellBot.Handlers
{
    public class Constants
    {
        public static string Ip = "http://122.167.240.91:3000";
        public static string GetFromNameLink = Ip + "/api/location/name/";
        public static string GetFromEmailLink = Ip + "/api/location/email/";
    }
}
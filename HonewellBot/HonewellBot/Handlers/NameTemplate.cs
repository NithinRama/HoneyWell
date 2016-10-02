using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonewellBot.Handlers
{

    public class NameTemplate
    {
        public Mobile[] mobile { get; set; }
        public Laptop[] laptop { get; set; }
    }

    public class Mobile
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string imei { get; set; }
        public Location1 location1 { get; set; }
        public string time1 { get; set; }
        public Location2 location2 { get; set; }
        public string time2 { get; set; }
        public Location3 location3 { get; set; }
        public string time3 { get; set; }
    }

    public class Location1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string routerMac { get; set; }
    }

    public class Laptop
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string deviceMac { get; set; }
        public Location1 location1 { get; set; }
        public string time1 { get; set; }
        public Location2 location2 { get; set; }
        public string time2 { get; set; }
        public Location3 location3 { get; set; }
        public string time3 { get; set; }
    }

    public class Location2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string routerMac { get; set; }
    }

    public class Location3
    {
        public int id { get; set; }
        public string name { get; set; }
        public string routerMac { get; set; }
    }

}
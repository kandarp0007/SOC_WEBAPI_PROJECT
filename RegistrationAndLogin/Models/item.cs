using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationAndLogin.Models
{
    public class item
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int price { get; set; }

        public string category { get; set; }
    }
}
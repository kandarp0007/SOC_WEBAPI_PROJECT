using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RegistrationAndLogin.Models
{
    public class Order
    {
        public int id { get; set; }

        public string personname { get; set; }

        public string orderitems { get; set; }

        public string comments { get; set; }

        public int totalammount { get; set; }

        public string ordertime { get; set; }
    }
}
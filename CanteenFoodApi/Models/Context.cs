using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CanteenFoodApi.Models
{
    public class Context : DbContext
    {
        public Context() : base("defaultconnectionstring")
        {

        }

        public DbSet<Item> Items { get; set; }


        public DbSet<Order> Orders { get; set; }
    }
}
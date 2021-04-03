using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CanteenFoodApi.Models;

namespace CanteenFoodApi.Controllers
{
    public class ItemsController : ApiController
    {
        Context db = new Context();

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return db.Items.ToList();
        }

        [HttpGet]
        public Item GetItem(int id)
        {
            return db.Items.Find(id);
        }

        [HttpPost]
        public HttpResponseMessage AddItem(Item item)
        {
            try
            {
                db.Items.Add(item);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage updateItem(int id, Item item)
        {
            try
            {
                if(id == item.id)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpDelete]
        public HttpResponseMessage deleteitem(int id)
        {
            Item item = db.Items.Find(id);
            if(item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CanteenFoodApi.Models;

namespace CanteenFoodApi.Controllers
{
    public class OrdersController : ApiController
    {
        Context db = new Context();

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders.ToList();
        }

        public Order GetOrderDetail(int id)
        {
            return db.Orders.Find(id);
        }

        [HttpPost]
        public HttpResponseMessage PlaceOrder(Order order)
        {
            try
            {
                db.Orders.Add(order);
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

        [HttpDelete]
        public HttpResponseMessage CancelOrder(int id)
        {
            Order order= db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
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

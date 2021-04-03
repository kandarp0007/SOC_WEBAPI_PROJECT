using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RegistrationAndLogin.Models;



namespace RegistrationAndLogin.Controllers
{
    public class HomeController : Controller
    {
        Uri baseaddress = new Uri("https://localhost:44390/api");
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            List<item> items = new List<item>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/items").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<item>>(data);
            }
            return View(items);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(string name , string order , string total, string comments)
        {

            DateTime now = DateTime.Now;
            string time = now.ToString();
            Order o = new Order();
            o.personname = name;
            o.orderitems = order;
            o.totalammount = int.Parse(total);
            o.comments = comments;
            o.id = 0;
            o.ordertime = time;
            string data = JsonConvert.SerializeObject(o);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/orders", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Checkout");
            }
            return View();
        }




        [Authorize]
        public ActionResult Admin()
        {
            List<item> items = new List<item>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/items").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<item>>(data);
            }
            return View(items);
        }

        [Authorize]
        public ActionResult AllOrders()
        {
            List<Order> items = new List<Order>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/orders").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<Order>>(data);
            }
            return View(items);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(item newitem)
        {
            string data = JsonConvert.SerializeObject(newitem);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/items", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Admin");
            }
            return View();
        }


        [Authorize]
        public ActionResult Edit(int id)
            {
            item items = new item();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/items/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<item>(data);
            }
            return View("Create", items);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(item newitem)
        {
            string data = JsonConvert.SerializeObject(newitem);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/items/" + newitem.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Admin");
            }
            return View("Create", newitem);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/items/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("Admin");
        }

        [Authorize]
        public ActionResult DeleteOrder(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/orders/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AllOrders");
            }
            return RedirectToAction("AllOrders");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return View();
        }

        private List<item> Getitems()
        {
            List<item> items = new List<item>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/items").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<item>>(data);
            }
            return items;
        }
    }
}
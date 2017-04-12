using RestaurantManagementProject.Models.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantManagementProject.Controllers
{
    public class ServerController : Controller
    {
        Entities db = new Entities();

        // GET: Server
        [HttpGet]
        public ActionResult Index()
        {            
            return View(new TableStatusViewModel(db.Tables.ToList()));
        }


        [HttpGet]
        public ActionResult InputOrder(int tableID)
        {
            return View(new InputOrderViewModel(tableID));
        }


        [HttpPost]
        public ActionResult InputOrder(List<FoodItem> foodItems,
            int tableID)
        {
            Order o = new Order();
            o.State = "Open";
            o.Table = db.Tables.FirstOrDefault(x=> x.Id == tableID);
            o.FoodItems = foodItems;

            decimal price = 0;
            foreach(FoodItem item in o.FoodItems)
                price += item.Price;
            o.TotalPrice = price.ToString();
            o.TimeCreated = DateTime.Now;


            o.Id = new Random().Next(0, int.MaxValue);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return RedirectToAction("TableStatus", "Server");
        }


        [HttpPost]
        public ActionResult CashoutCustomer(List<int> orderIds)
        {
            foreach (int id in orderIds)
            {
                Order o = db.Orders.FirstOrDefault(x => x.Id == id);
                o.TimeCompleted = DateTime.Now;
            }


            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return View();
        }


        [HttpPost]
        public ActionResult UpdateTable(int tableId, string status)
        {
            Table table = db.Tables.FirstOrDefault(x => x.Id == tableId);
            table.TableStatus = status;

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();


            return View();
        }

    }
}
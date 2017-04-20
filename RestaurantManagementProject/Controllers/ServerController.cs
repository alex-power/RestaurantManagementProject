using RestaurantManagementProject.Models.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantManagementProject.Controllers
{
    public class ServerController : BaseController
    {
        Entities db = new Entities();

        // GET: Server
        [HttpGet]
        public ActionResult Index()
        {            
            return View(new TableStatusViewModel(db.Tables.ToList()));
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult TableStatus()
        {
            return View();
        }

        public ActionResult InputOrder(int tableID)
        {
            var order = db.Orders.FirstOrDefault(x => x.Table.Id == tableID && x.State != "Complete");
            if (order != null) {
                return View(new InputOrderViewModel(tableID, db.FoodItems.ToList(), order));
            }
            else
            {
                //Different States of Orders is Open, Ready and Complete
                Order newOrder = new Order();
                newOrder.TimeCreated = DateTime.Now;
                newOrder.Table = db.Tables.FirstOrDefault(x => x.Id == tableID);
                newOrder.State = "Open";

                decimal price = 0;
                foreach (FoodItem item in newOrder.FoodItems)
                {
                    price += item.Price;
                }
                newOrder.TotalPrice = price.ToString();
                

                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.Orders.Add(newOrder);
                db.SaveChanges();
                db.Database.Connection.Close();

                return View(new InputOrderViewModel(tableID, db.FoodItems.ToList(), newOrder));
            }
        }

        [HttpGet]
        public ActionResult AddOrder(int foodId, int tableId, int orderId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            var item = db.FoodItems.FirstOrDefault(x => x.Id == foodId);
            order.FoodItems.Add(item);
            decimal tempPrice = Convert.ToDecimal(order.TotalPrice);
            tempPrice += item.Price;
            order.TotalPrice = Convert.ToString(tempPrice);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return RedirectToAction("InputOrder", "Server", new { tableId = tableId });
        }

        [HttpGet]
        public ActionResult DeleteItem(int itemId, int orderId, int tableId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            var item = db.FoodItems.FirstOrDefault(x => x.Id == itemId);
            order.FoodItems.Remove(item);
            decimal tempPrice = Convert.ToDecimal(order.TotalPrice);
            tempPrice -= item.Price;
            order.TotalPrice = Convert.ToString(tempPrice);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return RedirectToAction("InputOrder", "Server", new { tableId = tableId });
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


        [HttpGet]
        public ActionResult UpdateTable(int tableId, string status)
        {
            Table table = db.Tables.FirstOrDefault(x => x.Id == tableId);
            table.TableStatus = status;

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return RedirectToAction("Index", "Server");

        }

        [HttpGet]
        public ActionResult RemoveOrder(int orderId, int tableId)
        {
            Order o = db.Orders.FirstOrDefault(x => x.Id == orderId);
            o.State = "Closed";

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return RedirectToAction("InputOrder", "Server", new { tableId = tableId });
        }

    }
}
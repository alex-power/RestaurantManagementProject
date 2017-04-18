using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RestaurantManagementProject.Models.Kitchen;

namespace RestaurantManagementProject.Controllers
{
    public class KitchenController : BaseController
    {

        private Entities db = new Entities();

        // GET: Kitchen
        public ActionResult Index()
        {
            // Order states
            // Open = posted by server
            // Ready = ready to be picked up by server
            // Complete = delivered
            var orders = db.Orders.Where(x => x.State.Equals("Open"));
            List<Order> orderList = new List<Order>(orders);

            var readyOrders = db.Orders.Where(x => x.State.Equals("Ready"));
            List<Order> readyOrdersList = new List<Order>(readyOrders);


            return View(new KitchenViewModel(orderList, readyOrdersList));
        }

        /*
         *          MarkOrderReady
         *          Sets the order with that Id as ready.
         */
        [HttpGet]
        public ActionResult UpdateOrderStatus(int orderId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return RedirectToAction("Index");

            if (order.State.Equals("Ready"))
            {
                order.State = "Complete";
                order.TimeCompleted = DateTime.Now;
            }
            else
                order.State = "Ready";

            

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            // update the view models
            return RedirectToAction("Index");
        }

        /*
         *          RecallOrder
         *          Sets the order with that Id from ready to open
         */
        [HttpGet]
        public ActionResult RecallOrder(int orderId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return RedirectToAction("Index");

            // potentially here we could spout an error message if the order is completed
            if (order.State.Equals("Ready"))
                order.State = "Open";
            else
                return RedirectToAction("Index");


            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return RedirectToAction("Index");
        }


        

    }

}
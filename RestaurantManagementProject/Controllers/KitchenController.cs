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
            var orders = db.Orders.Where(x => !x.State.Equals("Complete"));
            List<Order> orderList = new List<Order>(orders);
       
                
            return View(new KitchenViewModel(orderList));
        }

        /*
         *          MarkOrderReady
         *          Sets the order with that Id as ready.
         */
        public ActionResult MarkOrderReady(int orderId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
            order.State = "Ready";
            order.TimeCompleted = DateTime.Now;

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            // update the view models
            return Index();
        }

        /*
         *          RecallOrder
         *          Sets the order with that Id from ready to open
         */
        public ActionResult RecallOrder(int orderId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);

            // potentially here we could spout an error message if the order is completed
            if (order.State.Equals("Ready"))
                order.State = "Open";
            else
                return Index();
            

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return Index();
        }


        

    }

}
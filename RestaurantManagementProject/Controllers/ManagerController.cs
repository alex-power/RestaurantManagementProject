using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManagementProject.Models.Manager;

namespace RestaurantManagementProject.Controllers
{
    public class ManagerController : BaseController
    {

        private Entities db = new Entities();

        public ActionResult ManagerView()
        {
            return View();
        }

        // GET: Manager
        public ActionResult Index()
        {
            List<Order> orders = db.Orders.Where(x => x.State.Equals("Completed")).ToList();

            double average = 0;
            foreach(Order order in orders)
            {
                DateTime start = order.TimeCreated;

                if (order.TimeCompleted == null)
                    continue;

                DateTime end = (DateTime)order.TimeCompleted.Value;

                TimeSpan delta = end.Subtract(start);
                average += delta.TotalMinutes;
            }

            average /= (double)orders.Count();

            int numOrders = orders.Count();

            // This may be handy
            List<Order> recentOrders = new List<Order>();
            foreach (Order order in orders)
            {
                DateTime end = (DateTime)order.TimeCompleted;
                TimeSpan delta = end.Subtract(DateTime.Now);

                if (delta.TotalHours <= 24.0)
                    recentOrders.Add(order);
            }

            int numOrdersLast24Hrs = recentOrders.Count();

            return View(new ManagerViewModel(numOrdersLast24Hrs,average,recentOrders));
        }


        [HttpPost]
        public ActionResult ApplyDiscount(int orderId, decimal discount)
        {
            Order o = db.Orders.FirstOrDefault(x => x.Id == orderId);
            o.TotalPrice 
                = (Convert.ToDecimal(o.TotalPrice) - (Convert.ToDecimal(o.TotalPrice) * discount)).ToString();

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            return View();
        }
        //change
        public ActionResult ClockIn()
        {
            return View();
        }
    }
}
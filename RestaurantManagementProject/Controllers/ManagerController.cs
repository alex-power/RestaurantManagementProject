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
            var userList = db.Users.OrderByDescending(x => x.Name);
            List<User> users = new List<User>(userList);

            
            return View(users);
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            
            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("ManagerView");
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

        [HttpGet]
        public ActionResult AddTable()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTable(int seats)
        {
            Table t = new Table();
            t.Seats = seats;
            t.TableStatus = "Open";
            db.Tables.Add(t);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();
            return RedirectToAction("ManagerView", "Manager");
        }

        [HttpGet]
        public ActionResult ChangeEmployeeStatus(int statusID, int userId)
        {
            User u = db.Users.FirstOrDefault(x => x.Id == userId);
            if (u.Users_Employee == null)
            {
                Users_Employee employee = new Users_Employee();
                employee.Id = userId;
                employee.HoursPerWeek = 40;
                db.Users_Employee.Add(employee);

                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.SaveChanges();
                db.Database.Connection.Close();
            }
            else
            {
                if (u.Users_Employee.Users_Kitchen != null)
                {
                    db.Users_Kitchen.Remove(u.Users_Employee.Users_Kitchen);
                }
                if (u.Users_Employee.Users_Server != null)
                {
                    db.Users_Server.Remove(u.Users_Employee.Users_Server);
                }
                if (u.Users_Employee.Users_Manager != null)
                {
                    db.Users_Manager.Remove(u.Users_Employee.Users_Manager);
                }
            }

            if(statusID == 0)
            {
                Users_Server user = new Users_Server();
                user.Id = userId;
                
                db.Users_Server.Add(user);

                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.SaveChanges();
                db.Database.Connection.Close();
            }
            else if (statusID == 1)
            {
                Users_Kitchen user = new Users_Kitchen();
                user.Id = userId;
                user.Role = "Chef";
                db.Users_Kitchen.Add(user);
                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.SaveChanges();
                db.Database.Connection.Close();
            }
            else
            {
                Users_Manager user = new Users_Manager();
                user.Id = userId;
                
                db.Users_Manager.Add(user);

                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.SaveChanges();
                db.Database.Connection.Close();
            }

            return RedirectToAction("ManagerView", "Manager");
        }

        public ActionResult Reservations()
        {
            return View(db.Reservations.Where(x => x.DateTime.Day.Equals(DateTime.Now.Day)).ToList());
        }
    }
}
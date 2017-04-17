using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantManagementProject.Controllers
{
    public class CustomersController : BaseController
    {
        private Entities db = new Entities();
        
        public ActionResult About()
        {
            return View();
        }

        public ActionResult ContactRestaurant()
        {
            return View();
        }

        public ActionResult CreateReservation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateReservation(DateTime time, string note)
        {
            Reservation r = new Reservation();
            r.DateTime = time;
            r.Note = note;
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.SaveChanges();
            db.Database.Connection.Close();
            return View();            
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult LeaveReview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeaveReview(DateTime visit, int rating, string text)
        {
            Review r = new Review();
            r.DateOfVisit = visit;
            r.DateOfPost = DateTime.Now;
            r.Rating = rating;
            r.Text = text;

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.SaveChanges();
            db.Database.Connection.Close();

            return View();
        }

    }
}
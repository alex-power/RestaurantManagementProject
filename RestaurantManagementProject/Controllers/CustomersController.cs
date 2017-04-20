using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace RestaurantManagementProject.Controllers
{
    public class CustomersController : BaseController
    {
        private Entities db = new Entities();

        public ActionResult ViewMenu()
        {
            return View();
        }

        public ActionResult ContactRestaurant()
        {
            return View();
        }


        //Create Reservation Section
        [HttpGet]
        public ActionResult CreateReservation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateReservation(string name, string note, string date, string time)
        {

            DateTime reserveTime = Convert.ToDateTime(date + " " + time);
            if (note == null)
                note = "  ";

            Reservation r = new Reservation();
            r.CustomerName = name;
            r.Note = note;
            r.DateTime = reserveTime;

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Reservations.Add(r);

            db.SaveChanges();
            db.Database.Connection.Close();

            return View();
        }



        public ActionResult Home()
        {
            return View();
        }


        //Leave Review Section
        public ActionResult LeaveReview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeaveReview(string name, string review, int rating)
        {
            Review r = new Review();
            r.CustomerName = name;
            r.DateOfVisit = DateTime.Now; //Doesn't matter, just keeping the db happy
            r.DateOfPost = DateTime.Now;
            r.Rating = rating;
            r.Text = review;

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Reviews.Add(r);
            db.SaveChanges();
            db.Database.Connection.Close();

            return View();
        }

        //Deleted the link, so this will never get called.
        public ActionResult About()
        {
            return View();
        }

    }
}
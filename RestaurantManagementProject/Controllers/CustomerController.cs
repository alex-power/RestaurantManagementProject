using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantManagementProject.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult CreateReservation()
        {
            Models.ReservationDb reservation = new Models.ReservationDb();


            return View(reservation.ListReservations());
        }
    }
}
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
        public ActionResult InputOrder(List<FoodItem> foodItems, int tableID)
        {
            Order o = new Order();
            o.State = "Open";
            o.Table = db.Tables.FirstOrDefault(x=> x.Id == tableID);
            o.FoodItems = foodItems;

            return RedirectToAction("TableStatus", "Server");
        }

    }
}
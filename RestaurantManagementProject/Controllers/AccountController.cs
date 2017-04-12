using RestaurantManagementProject.Auth;
using RestaurantManagementProject.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace RestaurantManagementProject.Controllers
{
    public class AccountController : BaseController
    {
        private Entities db = new Entities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User user = db.Users.FirstOrDefault(x => x.Username == username);
            
            if (user == null || user.Password != password)
                return View();
            else
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.Id = user.Id;
                serializeModel.Username = user.Username;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string email, string name)
        {
            User user = new RestaurantManagementProject.User();
            user.Username = username;
            user.Password = password;
            user.Email = email;
            user.Name = name;
            user.CreationDate = DateTime.Now;
            

            db.Users.Add(user);

            Users_Customer cust = new Users_Customer();
            cust.Id = user.Id;
            
            db.Users_Customer.Add(cust);

            db.Database.Connection.Open();
            db.SaveChanges();
            db.Database.Connection.Close();


            return RedirectToAction("Index");
        }
    }
}
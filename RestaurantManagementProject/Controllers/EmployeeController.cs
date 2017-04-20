using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManagementProject.Models.Employee;

namespace RestaurantManagementProject.Controllers
{
    public class EmployeeController : BaseController
    {

        private Entities db = new Entities();

        // GET: Employee
        public ActionResult Index()
        {

            //TODO: find the user ID
            int employeeId = 2;
            if (User != null)
                employeeId = User.Id;

            EmployeeViewModel model = new EmployeeViewModel();

            model.employeeId = employeeId;
            // get most recent timesheet

            List<DateTime> employeeTimeInTimes = new List<DateTime>();
            List<Timesheet> timesheets = db.Timesheet.ToList();
            foreach (Timesheet timesheet in timesheets)
                if (timesheet.Users_Employee.Id == employeeId)
                    employeeTimeInTimes.Add(timesheet.TimeIn);

            var all_timesheets = db.Timesheet.Where(x => x.Users_Employee.Id == employeeId);
            model.hoursWorkedTotal = 0;
            if (all_timesheets != null && all_timesheets.Count() > 0)
            {
                model.timesheets = all_timesheets.ToList();

                foreach(Timesheet t in model.timesheets)
                {
                    if (t.TimeOut.Equals(t.TimeIn) ||
                        t.TimeIn.Equals(DateTime.MinValue) ||
                        t.TimeOut.Equals(DateTime.MinValue))
                        continue;

                    var delta = t.TimeOut.Subtract(t.TimeIn);
                    model.hoursWorkedTotal += delta.Hours + (delta.Minutes / 60.0) + (delta.Seconds / (60 * 60));
                }
            }


           

            if (employeeTimeInTimes.Count() > 0)
            {
                DateTime latestDate = employeeTimeInTimes.Max();
                Timesheet timesheet = timesheets.FirstOrDefault(x => x.Users_Employee.Id == employeeId && x.TimeIn.Equals(latestDate));              

                model.timesheet = timesheet;
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult ClockIn()
        {
            int employeeId = 2;
            if (User != null)
                employeeId = User.Id;

            Timesheet timesheet = new Timesheet();
            DateTime now = DateTime.Now;
            timesheet.TimeIn = now;

            // couldn't figure out how to make this null, so I'm going to set this to the same thing
            // and then check for that in the index.
            timesheet.TimeOut = now;
            timesheet.Users_Employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);
            

            db.Timesheet.Add(timesheet);


            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();


            return RedirectToAction("Index", "Employee"); 
        }


        [HttpPost]
        public ActionResult ClockOut()
        {

            int employeeId = 2;
            if (User != null)
                employeeId = User.Id;

            Users_Employee employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);

            // get most recent timesheet
            List<DateTime> employeeTimeInTimes = new List<DateTime>();
            List<Timesheet> timesheets = db.Timesheet.ToList();
            foreach (Timesheet ts in timesheets)
                if (ts.Users_Employee.Id == employeeId)
                    employeeTimeInTimes.Add(ts.TimeIn);

            DateTime latestDate = employeeTimeInTimes.Max();
            Timesheet timesheet = timesheets.FirstOrDefault(x => x.Users_Employee.Id == employeeId && x.TimeIn.Equals(latestDate));

            
            timesheet.TimeOut = DateTime.Now;
            timesheet.Users_Employee = employee;

            // timesheets only added to user after they are completed
            //employee.Timesheets.Add(timesheet);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();
            

            return RedirectToAction("Index", "Employee");
        }

    }
}
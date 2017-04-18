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


            if (employeeTimeInTimes.Count() > 0)
            {
                DateTime latestDate = employeeTimeInTimes.Max();
                Timesheet timesheet = timesheets.FirstOrDefault(x => x.Users_Employee.Id == employeeId && x.TimeIn.Equals(latestDate));

                model.timesheet = timesheet;


                if (model.timesheet != null 
                    && model.timesheet.TimeOut != null 
                    && model.timesheet.TimeIn.Equals(model.timesheet.TimeOut))
                {
                    model.timesheet.TimeOut = new DateTime();
                }
                else
                {

                    var delta = model.timesheet.TimeOut.Subtract(model.timesheet.TimeIn);

                    model.hoursWorkedTotal = delta.Hours + (delta.Minutes / 60.0) + (delta.Seconds / (60 * 60));
                }

            }

            /*

model.schedules = db.WorkSchedules.ToList();
model.employeeId = employeeId;
model.hoursWorkedTotal = 0;

// get most recent timesheet

List<DateTime> employeeTimeInTimes = new List<DateTime>();
List<Timesheet> timesheets = db.Timesheet.ToList();
foreach (Timesheet timesheet in timesheets)
    if (timesheet.Users_Employee.Id == employeeId)
        employeeTimeInTimes.Add(timesheet.TimeIn);

DateTime latestDate = employeeTimeInTimes.Max();

model.timesheet = db.Timesheet.FirstOrDefault(x => x.Users_Employee.Id == employeeId && x.TimeIn.Equals(latestDate));
*/
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


        [HttpPost]
        public ActionResult GetTotalHoursWorked(int employeeId)
        {

            Users_Employee employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);
            List<Timesheet> timesheets = employee.Timesheets.ToList();

            double hoursWorked = 0;
            foreach (Timesheet time in timesheets)
            {
                DateTime start = time.TimeIn;
                if (time.TimeOut == null)
                    continue;

                DateTime end = (DateTime)time.TimeOut;

                hoursWorked += end.Subtract(start).TotalHours;
            }

            EmployeeViewModel model = new EmployeeViewModel();
            model.schedules = db.WorkSchedules.ToList();
            model.employeeId = employeeId;
            model.timesheet = new Timesheet();
            model.hoursWorkedTotal = hoursWorked;


            return View();
        }
    }
}
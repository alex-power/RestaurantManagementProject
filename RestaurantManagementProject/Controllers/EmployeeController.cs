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
            int employeeId = User.Id;

            EmployeeViewModel model = new EmployeeViewModel();
            model.schedules = db.WorkSchedules.ToList();
            model.employeeId = employeeId;
            model.hoursWorkedTotal = 0;

            // get most recent timesheet
            List<DateTime> employeeTimeInTimes = new List<DateTime>();
            List<Timesheet> timesheets = db.Timesheets.ToList();
            foreach (Timesheet timesheet in timesheets)
                if (timesheet.Users_Employee.Id == employeeId)
                    employeeTimeInTimes.Add(timesheet.TimeIn);

            DateTime latestDate = employeeTimeInTimes.Max();

            model.timesheet = db.Timesheets.FirstOrDefault(x => x.Users_Employee.Id == employeeId && x.TimeIn.Equals(latestDate));

            return View(model);
        }


        [HttpPost]
        public ActionResult ClockIn(int employeeId)
        {
            Timesheet timesheet = new Timesheet();
            timesheet.TimeIn = DateTime.Now;
            timesheet.Users_Employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);

            db.Timesheets.Add(timesheet);


            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();


            return RedirectToAction("Index", "Employee"); 
        }


        [HttpPost]
        public ActionResult ClockOut(int employeeId)
        {

            Users_Employee employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);

            // get most recent timesheet
            List<DateTime> employeeTimeInTimes = new List<DateTime>();
            List<Timesheet> timesheets = db.Timesheets.ToList();
            foreach (Timesheet ts in timesheets)
                if (ts.Users_Employee.Id == employeeId)
                    employeeTimeInTimes.Add(ts.TimeIn);

            DateTime latestDate = employeeTimeInTimes.Max();
            Timesheet timesheet = db.Timesheets.FirstOrDefault(x => x.Users_Employee.Id == employeeId && x.TimeIn.Equals(latestDate));

            
            timesheet.TimeOut = DateTime.Now;
            timesheet.Users_Employee = employee;

            // timesheets only added to user after they are completed
            employee.Timesheets.Add(timesheet);

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
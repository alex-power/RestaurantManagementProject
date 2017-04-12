using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManagementProject.Models.Employee;

namespace RestaurantManagementProject.Controllers
{
    public class EmployeeController : Controller
    {

        private Entities db = new Entities();

        // GET: Employee
        public ActionResult Index()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.schedules = db.WorkSchedules.ToList();


            return View(model);
        }


        [HttpPost]
        public ActionResult ClockIn(int employeeId)
        {
            Timesheet timesheet = new Timesheet();
            timesheet.Id = new Random().Next(0, int.MaxValue);
            timesheet.TimeIn = DateTime.Now;
            timesheet.Users_Employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            EmployeeViewModel model = new EmployeeViewModel();
            model.schedules = db.WorkSchedules.ToList();
            model.employeeId = employeeId;
            model.timesheet = timesheet;
            model.hoursWorkedTotal = 0; 


            return View(model);
        }


        [HttpPost]
        public ActionResult ClockOut(int employeeId)
        {

            Users_Employee employee = db.Users_Employee.FirstOrDefault(x => x.Id == employeeId);
            Timesheet timesheet = db.Timesheets.FirstOrDefault(x => x.Users_Employee.Id == employeeId);
            timesheet.TimeOut = DateTime.Now;
            timesheet.Users_Employee = employee;

            // timesheets only added to user after they are completed
            employee.Timesheets.Add(timesheet);

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.SaveChanges();
            db.Database.Connection.Close();

            EmployeeViewModel model = new EmployeeViewModel();
            model.schedules = db.WorkSchedules.ToList();
            model.employeeId = employeeId;
            model.timesheet = timesheet;
            model.hoursWorkedTotal = 0;
            

            return View(model);
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
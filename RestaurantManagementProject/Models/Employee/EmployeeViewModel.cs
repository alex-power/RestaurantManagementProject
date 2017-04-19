using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Employee
{
    public class EmployeeViewModel
    {
        public int employeeId { get; set; }
        public double hoursWorkedTotal { get; set; }
        public Timesheet timesheet { get; set; }
        public List<Timesheet> timesheets { get; set; }

        public EmployeeViewModel()
        {
            employeeId = 0;
            hoursWorkedTotal = 0;
            timesheet = new Timesheet();
            timesheets = new List<Timesheet>();
        }

    }
}
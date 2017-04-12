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
        public double hoursWorkedToday { get; set; }
        public List<WorkSchedule> schedules { get; set; }
        public Timesheet timesheet { get; set; }

        public EmployeeViewModel()
        {
            employeeId = 0;
            hoursWorked = 0;
            schedules = new List<WorkSchedule>();
            timesheet = new Timesheet();
        }

    }
}
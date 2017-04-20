using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Manager
{
    public class ManagerEmployeeViewModel
    {
        public List<User> Users { get; set; }
        public Dictionary<int, string> userTypes { get; set; }
        public int EmployeeTypeId { get; set; }

        public ManagerEmployeeViewModel(List<User> u)
        {
            Users = u;
            userTypes = new Dictionary<int, string>();
            userTypes.Add(0, "Server");
            userTypes.Add(1, "Kitchen");
            userTypes.Add(2, "Manager");
        }
    }
}
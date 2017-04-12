using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Server
{
    public class InputOrderViewModel
    {
        public Order Order { get; set; }
        public int TableID { get; set; }

        public InputOrderViewModel(int tableID)
        {
            TableID = tableID;
        }
    }
}
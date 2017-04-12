using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Manager
{
    public class ManagerViewModel
    {
        public int numOrdersLast24Hrs { get; set; }
        public double averageOrderTime { get; set; }

        public List<Order> recentOrders { get; set; }


        public ManagerViewModel(int numOrders, double avgOrderTime, List<Order> orders)
        {
            numOrdersLast24Hrs = numOrders;
            averageOrderTime = avgOrderTime;
            recentOrders = orders;
        }

    }
}
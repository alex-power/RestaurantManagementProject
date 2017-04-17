using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Kitchen
{
    public class KitchenViewModel
    {
        public List<Order> OpenOrders;

        public List<Order> ReadyOrders;

        public KitchenViewModel(List<Order> openOrders, List<Order> readyOrders)
        {
            OpenOrders = openOrders;
            ReadyOrders = readyOrders;
        }

    }
}
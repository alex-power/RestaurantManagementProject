using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Kitchen
{
    public class KitchenViewModel
    {
        public List<Order> Orders;

        public KitchenViewModel(List<Order> orders)
        {
            Orders = orders;
        }

    }
}
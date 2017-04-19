﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Server
{
    public class InputOrderViewModel
    {
        public Order Order { get; set; }
        public int TableID { get; set; }
        public List<Order> Orders {get;set;}
        public List<FoodItemPartialModel> FoodItems { get;set;}

        public InputOrderViewModel(int tableID, List<FoodItem> foodItems, List<Order> orders)
        {
            TableID = tableID;
            Orders = orders;
            FoodItems = foodItems.Select(x => new FoodItemPartialModel(x)).ToList();
        }

        public InputOrderViewModel(int tableID, Order o)
        {
            TableID = tableID;
            Order = o;
        }

    }
}
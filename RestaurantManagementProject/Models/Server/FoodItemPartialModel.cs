using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantManagementProject.Models.Server
{
    public class FoodItemPartialModel
    {
        public FoodItem FoodItem { get; set; }
        public int Added { get; set; }

        public FoodItemPartialModel(FoodItem foodItem)
        {
            FoodItem = foodItem;
            Added = 0;
        }
    }
}
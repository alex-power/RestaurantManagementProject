using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantManagementProject.Models.Server
{
    public class TableStatusViewModel
    {
        public List<Table> Tables { get; set; }

        public TableStatusViewModel (List<Table> tables)
        {
            Tables = tables;
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantManagementProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timesheet
    {
        public int Id { get; set; }
        public System.DateTime TimeIn { get; set; }
        public System.DateTime TimeOut { get; set; }
    
        public virtual Users_Employee Users_Employee { get; set; }
    }
}

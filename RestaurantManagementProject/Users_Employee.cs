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
    
    public partial class Users_Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users_Employee()
        {
            this.WorkSchedules = new HashSet<WorkSchedule>();
        }
    
        public string Availability { get; set; }
        public int HoursPerWeek { get; set; }
        public Nullable<decimal> PayRate { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public int Id { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
        public virtual Users_Kitchen Users_Kitchen { get; set; }
        public virtual Users_Manager Users_Manager { get; set; }
        public virtual Users_Server Users_Server { get; set; }
    }
}
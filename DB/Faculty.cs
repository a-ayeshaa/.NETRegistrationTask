//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabTaskRegistration.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Faculty
    {
        public Faculty()
        {
            this.CourseFaculties = new HashSet<CourseFaculty>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public int DeptID { get; set; }
    
        public virtual ICollection<CourseFaculty> CourseFaculties { get; set; }
        public virtual Department Department { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegistrationAndLogin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string PrijectName { get; set; }
        public string SupervisorName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndTime { get; set; }
        public int UserID { get; set; }
    
        public virtual User User { get; set; }
    }
}
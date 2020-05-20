using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationAndLogin.Models.Extended
{

    [MetadataType(typeof(TaskMetadata))]
    public partial class Task
    {
    }
    public class TaskMetadata
    {


        [Display(Name = "Task name*")]
        [Required(ErrorMessage = "Please enter Task name!")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please enter a correct name!")]
        public string TaskName { get; set; }


        [Display(Name = "Project name*")]
        [Required(ErrorMessage = "Please enter Project name!")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please enter a correct name!")]
        public string PrijectName { get; set; }

        [Display(Name = "Supervisor name*")]
        [Required(ErrorMessage = "Please enter Supervisor name!")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please enter a correct name!")]
        public string SupervisorName { get; set; }

        [Display(Name = " Starting date")]
        [Required(ErrorMessage = "Please enter starting date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        // [LessThanEqualTo("EndDate", ErrorMessage = "The date must be lower than end date!")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = " End date ")]
        [Required(ErrorMessage = "Please enter ending date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime EndTime { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }
    }
}
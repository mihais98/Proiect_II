using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RegistrationAndLogin.Models;

namespace RegistrationAndLogin.ViewModels
{
    public class TaskViewModel
    {
        public Task Task { get; set; }
        public string Message { get; }

        public bool Status { get; }

        //Constructor for GET

        public TaskViewModel()
        {


        }

    }
}
using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationAndLogin
{
    public sealed class DatabaseCon
    {
        private static readonly MyDatabaseEntities7 context = new MyDatabaseEntities7();

         public MyDatabaseEntities7 Access()
        {
            return context;
        }
    }
}
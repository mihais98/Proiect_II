using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistrationAndLogin.Models.Extended;

namespace RegistrationAndLogin.Service
{
    interface ITaskService
    {
        public void Add(TaskMetadata task);
        public void Remove(Models.Task task);
    }
}

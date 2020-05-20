using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistrationAndLogin.Repository;
using RegistrationAndLogin.Models;

namespace RegistrationAndLogin.Repository
{
    public interface ITaskRepository
    {
        public void AddTask(Models.Task task);
        public Models.Task findById(int? id);
        public void Remove(Models.Task deleteTask);
    }
}

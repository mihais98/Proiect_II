using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RegistrationAndLogin.Models;
using RegistrationAndLogin.Repository;

namespace RegistrationAndLogin.Repository
{
    public class TaskRepository : ITaskRepository
    {

       
        public void AddTask(Models.Task task)
        {

            DatabaseCon con = new DatabaseCon();

            var context = con.Access();
           
                context.Tasks.Add(task);
                    context.SaveChanges();
                
             
           
        }

        public Models.Task findById(int? id)
        {
            Models.Task foundTask = new Models.Task();

            DatabaseCon con = new DatabaseCon();

            var context = con.Access();

            
                    foundTask = context.Tasks.FirstOrDefault(x => x.TaskID == id);
                
            
           

            return foundTask;
        }

        public void Remove(Models.Task deleteTask)
        {
            try
            {
                DatabaseCon con = new DatabaseCon();

                var context = con.Access();
                context.Tasks.Attach(deleteTask);
                    context.Tasks.Remove(deleteTask);
                    context.SaveChanges();
                
            }
            catch (System.Exception e)
            {
                //Logger.Info(e.Message);
               // throw new DatabaseException("Cannot connect to database!\n");
            }
        }

        public void Remove(System.Threading.Tasks.Task deleteTask)
        {
            throw new NotImplementedException();
        }
    }
}
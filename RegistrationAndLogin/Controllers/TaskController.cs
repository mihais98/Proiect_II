using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationAndLogin.Models;
using RegistrationAndLogin.Service;
using RegistrationAndLogin.Repository;
using RegistrationAndLogin.Models.Extended;

namespace RegistrationAndLogin.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUserService userService;
        private readonly TaskRepository taskRepository = new TaskRepository();
        private readonly ITaskService taskService = new TaskService(new TaskRepository());
        DatabaseCon con = new DatabaseCon();
        MyDatabaseEntities7 db;
        
       

        // GET: Task

        public TaskController()
        {
            userService = new UserService(new UserRepository());
            db = con.Access();
        }

        public ActionResult Index()
        {
            var identity = User.Identity.Name;
            User user = userService.FindByEmail(identity);

            if (user != null)
            {
                return View(db.Tasks.Where(t => t.UserID == user.UserID));
            }
            else
            {
                return View();
            }

        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Exclude = "User")] TaskMetadata task)
        {
            var identity = User.Identity.Name;
            User user = userService.FindByEmail(identity);

            task.User = user;
            task.UserID = user.UserID;


            taskService.Add(task);      
            

            return RedirectToAction("Index");
}

        [Authorize]
        
        public ActionResult Delete(int? id)
        {

            var task = taskRepository.findById(id);

            return PartialView("_Delete",task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {

            
            Models.Task deleteTask = taskRepository.findById(id);
            taskService.Remove(deleteTask);

            return RedirectToAction("Index");
        }







    }
}
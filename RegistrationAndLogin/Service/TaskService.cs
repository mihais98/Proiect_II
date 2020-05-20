using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RegistrationAndLogin.Repository;
using RegistrationAndLogin.Models;
using RegistrationAndLogin.Models.Extended;
namespace RegistrationAndLogin.Service
{
    public class TaskService : ITaskService
    {
        ITaskRepository taskRepository;

        public TaskService()
        {
            taskRepository = new TaskRepository();
        }
        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public void Add(TaskMetadata task)
        {
            taskRepository.AddTask(ChangeTaskModel(task));
        }
        public Models.Task ChangeTaskModel(TaskMetadata task)
        {
            Models.Task newTask = new Models.Task();

            newTask.TaskName = task.TaskName;
            newTask.UserID = task.UserID;
            newTask.User = task.User;
            newTask.PrijectName = task.PrijectName;
            newTask.SupervisorName = task.SupervisorName;
            newTask.StartDate = task.StartDate;
            newTask.EndTime = task.EndTime;

            return newTask;
        }
        public void Remove(Models.Task task)
        {
            taskRepository.Remove(task);
        }
    }
}
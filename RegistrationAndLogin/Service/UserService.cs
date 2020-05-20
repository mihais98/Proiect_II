using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Net;
using System.Net.Mail;
using RegistrationAndLogin.Repository;


namespace RegistrationAndLogin.Service
{
    

    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool IsEmailExist(string emailID)
        {
            return userRepository.FindByEmail(emailID) != null;
        }

        public User FindByEmail(string email)
        {
            return userRepository.FindByEmail(email);
        }

       
    }
}
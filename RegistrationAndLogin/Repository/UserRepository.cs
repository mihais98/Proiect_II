using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RegistrationAndLogin.Models;



namespace RegistrationAndLogin.Repository
{
    public class UserRepository : IUserRepository
    {
        

        public IList<User> FindAll()
        {

            IList<User> users = new List<User>();

            DatabaseCon con = new DatabaseCon();

            var dc = con.Access();
           
            
            users = dc.Users.ToList();
            if (users != null)
            return users;

            else
                return null;
        }

        public User FindByEmail(string email)
        {
            IList<User> users = FindAll();

            return users.SingleOrDefault(x => x.EnailID == email);
        }

     //   public User Add()
    }
}
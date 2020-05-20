using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistrationAndLogin.Models;
using RegistrationAndLogin.Repository;

namespace RegistrationAndLogin.Repository
{
    public interface IUserRepository
    {
        public IList<User> FindAll();
        public User FindByEmail(string email);
    }

}

using Microsoft.AspNetCore.Http;
using SlabCode.DataAccess;
using SlabCode.DataAccess.DBOperations.Implementation;
using System;
using System.Security.Claims;

namespace SlabCode.Core.ProjectServices.Implementation
{
    public class UserServices
    {
        private readonly UserDbOperations userDbOperations;

        public UserServices(UserDbOperations userDbOperations)
        {
            this.userDbOperations = userDbOperations;
        }

        public Tuple<bool, string> login(string username, string password)
        {
            User user = userDbOperations.getById(username);

            if (user==null || !user.Username.Equals(username) || !user.Password.Equals(password) || user.Enable==false)
            {
                return new Tuple<bool, string>(false, string.Empty);                
            }
            return new Tuple<bool, string>(true, user.Role);
        }        

        public string createOperatorUser(User user)
        {
            userDbOperations.create(user);
            return $"Operator \"{user.Username}\" created";
        }        

        public string changePassword(string oldPassword, string newPassword, HttpContext httpContext)
        {
            try
            {
                var username = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = userDbOperations.getById(username);
                if (oldPassword.Equals(user.Password))
                {
                    user.Password = newPassword;
                    userDbOperations.update(user);
                    return "Password changed";
                }
                return "Invalid actual password";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public string enable(string username, bool enable)
        {
            User user = userDbOperations.getById(username);
            if (user.Role.Equals("Operador"))
            {
                user.Enable = enable;
                userDbOperations.update(user);

                if (enable) return $"User \"{username}\" enabled";
                else return $"User \"{username}\" disabled";
            }
            return $"You could'n enable or disable \"{username}\", is an administrator user";
        }
    }
}

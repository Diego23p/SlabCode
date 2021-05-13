using Microsoft.AspNetCore.Http;
using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;
using SlabCode.DataAccess.DBOperations.Implementation;
using System;
using System.Security.Claims;

namespace SlabCode.Core.ProjectServices.Implementation
{
    public class SlabCodeProjectManagement : IProjectManagement
    {
        private readonly UserDbOperations userDbOperations;
        private readonly ProjectDbOperations projectDbOperations;        

        public SlabCodeProjectManagement(UserDbOperations userDbOperations, ProjectDbOperations projectDbOperations)
        {
            this.userDbOperations = userDbOperations;
            this.projectDbOperations = projectDbOperations;
        }

        public Tuple<bool, string> loguin(string username, string password)
        {
            User user = userDbOperations.GetById(username);

            if (user==null || !user.Username.Equals(username) || !user.Password.Equals(password))
            {
                return new Tuple<bool, string>(false, string.Empty);                
            }
            return new Tuple<bool, string>(true, user.Role);
        }        

        public void createOperatorUser(User user)
        {
            userDbOperations.Insert(user);
        }        

        public string changePassword(string oldPassword, string newPassword, HttpContext httpContext)
        {
            try
            {
                var username = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = userDbOperations.GetById(username);
                if (oldPassword.Equals(user.Password))
                {
                    user.Password = newPassword;
                    userDbOperations.Update(user);
                    return "Password changed";
                }
                return "Invalid actual password";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public void createProyect(Project project)
        {
            projectDbOperations.create(project);
        }

        public void updateProyect(Project project)
        {
            Project projectDb = projectDbOperations.GetById(project.Name);

            projectDb.Name = project.Name;
            projectDb.Description = project.Description;
            projectDb.Initdate = project.Initdate;

            projectDbOperations.Update(projectDb);
        }
    }
}

using SlabCode.DataAccess;
using System;
using Microsoft.AspNetCore.Http;

namespace SlabCode.Core.ProjectServices.Contract
{
    public interface IProjectManagement
    {
        public Tuple<bool, string> loguin(string username, string password);
        public void createOperatorUser(User user);
        public string changePassword(string oldPassword, string newPassword, HttpContext httpContext);
        public void createProyect(Project project);
        public void updateProyect(Project project);
    }
}

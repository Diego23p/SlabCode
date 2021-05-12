using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlabCode.API.Controller
{
    [Authorize]
    [Route("Projects/")]
    public class ProjectsController
    {
        private readonly IProjectManagement ProjectManagement;

        public ProjectsController(IProjectManagement projectManagement)
        {
            ProjectManagement = projectManagement;
        }

        [HttpGet]
        [Authorize]//(Roles = "admin")]
        public string Hello([FromBody] User user)
        {
            ProjectManagement.getUsers(user);
            return "Ok";
        }
    }
}

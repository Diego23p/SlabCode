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
    [Route("Projects/")]
    public class ProjectsController
    {
        private readonly IProjectManagement ProjectManagement;

        public ProjectsController(IProjectManagement projectManagement)
        {
            ProjectManagement = projectManagement;
        }

        [HttpGet]
        [Route("createOperatorUser/")]
        [Authorize(Roles = "Administrador")]
        public string createOperatorUser([FromBody] User user)
        {
            ProjectManagement.createOperatorUser(user);
            return "Operator user created";
        }
    }
}

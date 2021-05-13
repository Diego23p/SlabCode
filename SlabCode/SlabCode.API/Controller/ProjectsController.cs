using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;

namespace SlabCode.API.Controller
{
    [Route("Projects/")]
    public class ProjectsController : ControllerBase
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

        [HttpPut]
        [Route("changePassword/")]
        [Authorize]
        public string changePassword(string oldPassword, string newPassword)
        {
            return ProjectManagement.changePassword(oldPassword, newPassword, HttpContext);
        }

        [HttpPost]
        [Route("createProyect/")]
        [Authorize(Roles = "Operador")]
        public string createProyect([FromBody] Project project)
        {
            ProjectManagement.createProyect(project);
            return "Project created";
        }

        [HttpPost]
        [Route("updateProyect/")]
        [Authorize(Roles = "Operador")]
        public string updateProyect([FromBody] Project project)
        {
            ProjectManagement.updateProyect(project);
            return "Project updated";
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlabCode.Core.ProjectServices.Implementation;
using SlabCode.DataAccess;
using System;

namespace SlabCode.API.Controller
{
    [Route("Project/")]
    [Authorize(Roles = "Operador")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectServices ProjectServices;

        public ProjectController(ProjectServices projectServices)
        {
            ProjectServices = projectServices;
        }

        [HttpPost]
        [Route("create/")]
        public string create([FromBody] Project project)
        {
            try
            {
                return ProjectServices.create(project);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }
        }

        [HttpPut]
        [Route("update/")]
        public string update([FromBody] Project project)
        {
            try
            {
                return ProjectServices.update(project);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }
        }

        [HttpDelete]
        [Route("delete/")]
        [Authorize(Roles = "Administrador")]
        public string delete(string id)
        {
            try
            {
                return ProjectServices.delete(id);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }
        }

        [HttpPut]
        [Route("complete/")]
        public string complete(string id)
        {
            try
            {
                return ProjectServices.complete(id);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }
        }
    }
}

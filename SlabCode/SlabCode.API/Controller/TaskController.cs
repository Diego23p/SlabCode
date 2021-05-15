using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlabCode.Core.ProjectServices.Implementation;
using SlabCode.DataAccess;
using System;

namespace SlabCode.API.Controller
{
    [Route("Task/")]
    [Authorize(Roles = "Operador")]
    public class TaskController : ControllerBase
    {
        private readonly TaskServices TaskServices;

        public TaskController(TaskServices taskServices)
        {
            TaskServices = taskServices;
        }

        [HttpPost]
        [Route("create/")]        
        public string createTask([FromBody] Task task)
        {
            try
            {
                return TaskServices.create(task);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }            
        }

        [HttpPost]
        [Route("update/")]
        public string update([FromBody] Task task)
        {
            try
            {
                return TaskServices.update(task);
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
                return TaskServices.delete(id);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }            
        }

        [HttpPut]
        [Route("complete/")]
        public string complete(string id)
        {
            try
            {
                return TaskServices.complete(id);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }            
        }
    }
}

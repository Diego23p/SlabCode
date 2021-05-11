using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        [Authorize(Roles = "admin")]
        public string Hello()
        {
            return $"Hello!";
        }
    }
}

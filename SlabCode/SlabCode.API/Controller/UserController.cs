using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlabCode.Core.ProjectServices.Implementation;
using SlabCode.DataAccess;
using System;

namespace SlabCode.API.Controller
{
    [Route("User/")]
    public class UserController : ControllerBase
    {
        private readonly UserServices UserServices;

        public UserController(UserServices userServices)
        {
            UserServices = userServices;
        }

        [HttpPost]
        [Route("createOperatorUser/")]
        [Authorize(Roles = "Administrador")]
        public string createOperatorUser([FromBody] User user)
        {
            try
            {
                return UserServices.createOperatorUser(user);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }
        }

        [HttpPut]
        [Route("changePassword/")]
        [Authorize]
        public string changePassword(string oldPassword, string newPassword)
        {
            try
            {
                return UserServices.changePassword(oldPassword, newPassword, HttpContext);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }

        }

        [HttpPut]
        [Route("enable/")]
        [Authorize(Roles = "Administrador")]
        public string enable(string username, bool enable)
        {
            try
            {
                return UserServices.enable(username, enable);
            }
            catch (Exception ex) { return $"Error {ex.Message}"; }
        }
    }
}

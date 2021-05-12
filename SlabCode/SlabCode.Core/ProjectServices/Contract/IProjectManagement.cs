using SlabCode.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SlabCode.Core.ProjectServices.Contract
{
    public interface IProjectManagement
    {
        public bool loguin(string username, string password);
        public void getUsers(User user);
    }
}

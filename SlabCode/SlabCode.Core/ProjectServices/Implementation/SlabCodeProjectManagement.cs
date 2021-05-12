using Microsoft.EntityFrameworkCore;
using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;
using System;
using System.Linq;

namespace SlabCode.Core.ProjectServices.Implementation
{
    public class SlabCodeProjectManagement : IProjectManagement
    {
        private readonly SlabCodeTestContext DbContext;

        public SlabCodeProjectManagement(SlabCodeTestContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public Tuple<bool, string> loguin(string username, string password)
        {
            User user = GetUserByIdAsync(username);

            if (user==null || !user.Username.Equals(username) || !user.Password.Equals(password))
            {
                return new Tuple<bool, string>(false, string.Empty);                
            }
            return new Tuple<bool, string>(true, user.Role);

        }

        public User GetUserByIdAsync(string username)
        {
            return DbContext.Users.Select(
                    s => new User
                    {
                        Username = s.Username,
                        Password = s.Password,
                        Role = s.Role,
                        Enable = s.Enable,
                        Email = s.Email
                    })
                .FirstOrDefaultAsync(s => s.Username == username).Result;
        }

        public void createOperatorUser(User user)
        {
            InsertUserAsync(user).Wait();
        }

        public async System.Threading.Tasks.Task InsertUserAsync(User User)
        {
            var entity = new User()
            {
                Username = User.Username,
                Password = User.Password,
                Role = "Operador",
                Enable = User.Enable,
                Email = User.Email
            };

            DbContext.Users.Add(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}

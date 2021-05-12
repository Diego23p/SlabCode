using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;

namespace SlabCode.Core.ProjectServices.Implementation
{
    public class SlabCodeProjectManagement : IProjectManagement
    {
        private readonly SlabCodeTestContext DbContext;

        public SlabCodeProjectManagement(SlabCodeTestContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public bool loguin(string username, string password)
        {
            return true;
        }

        public void getUsers(User user)
        {
            InsertUserAsync(user).Wait();
        }

        public async System.Threading.Tasks.Task InsertUserAsync(User User)
        {
            var entity = new User()
            {
                Username = User.Username,
                Password = User.Password,
                Role = User.Role,
                Enable = User.Enable,
                Email = User.Email
            };

            DbContext.Users.Add(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}

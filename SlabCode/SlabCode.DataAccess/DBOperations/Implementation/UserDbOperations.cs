using Microsoft.EntityFrameworkCore;
using SlabCode.DataAccess.DBOperations.Contract;
using System.Linq;

namespace SlabCode.DataAccess.DBOperations.Implementation
{
    public class UserDbOperations : IDbOperations
    {
        private readonly SlabCodeTestContext DbContext = new SlabCodeTestContext();

        public UserDbOperations(SlabCodeTestContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public User GetById(string id)
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
                .FirstOrDefault(s => s.Username == id);
        }

        public void Insert(User User)
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
            DbContext.SaveChanges();
        }

        public void Update(User user)
        {
            var entity = DbContext.Users.FirstOrDefault(s => s.Username == user.Username);

            entity.Username = user.Username;
            entity.Password = user.Password;
            entity.Role = user.Role;
            entity.Enable = user.Enable;
            entity.Email = user.Email;

            DbContext.SaveChanges();
        }
    }
}

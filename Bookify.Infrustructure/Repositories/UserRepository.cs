using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bookify.Infrustructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        //public override void Add(User user)
        //{
        //    foreach (Role role in user.Roles)
        //    {
        //        DbContext.Attach(role);
        //    }

        //    _dbContext.Add(user);
        //}

    }
}

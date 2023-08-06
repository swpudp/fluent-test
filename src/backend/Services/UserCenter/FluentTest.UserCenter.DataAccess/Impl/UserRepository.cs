using FluentTest.UserCenter.DataAccess.Contract;
using FluentTest.UserCenter.Model.Entity;

namespace FluentTest.UserCenter.DataAccess.Impl
{
    public class UserRepository : IUserRepository
    {
        public User GetById(string id)
        {
            return new User { Id = id, FullName = Guid.NewGuid().ToString("N") };
        }
    }
}
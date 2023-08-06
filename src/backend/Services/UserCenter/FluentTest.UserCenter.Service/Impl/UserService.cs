using FluentTest.UserCenter.DataAccess.Contract;
using FluentTest.UserCenter.Model.Entity;
using FluentTest.UserCenter.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.UserCenter.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }
    }
}

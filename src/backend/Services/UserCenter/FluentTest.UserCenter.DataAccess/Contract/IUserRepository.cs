using FluentTest.UserCenter.Model.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.UserCenter.DataAccess.Contract
{
    public interface IUserRepository : IUserStore<User>, IUserPasswordStore<User>
    {
        User GetById(string id);
    }
}

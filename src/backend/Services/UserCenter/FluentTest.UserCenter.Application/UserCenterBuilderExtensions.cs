using FluentTest.UserCenter.Application.Controllers;
using FluentTest.UserCenter.DataAccess.Contract;
using FluentTest.UserCenter.DataAccess.Impl;
using FluentTest.UserCenter.Service.Contract;
using FluentTest.UserCenter.Service.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.UserCenter.Application
{
    public static class UserCenterBuilderExtensions
    {
        public static IMvcBuilder AddUserCenterPart(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(UserController).Assembly);
            return builder;
        }

        public static IServiceCollection AddUserCenterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}

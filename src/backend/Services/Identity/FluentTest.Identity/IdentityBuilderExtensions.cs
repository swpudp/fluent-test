using FluentTest.Identity.Application;
using FluentTest.Identity.Policies;
using FluentTest.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity
{
    public static class IdentityBuilderExtensions
    {
        public static IMvcBuilder AddIdentityPart(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(UserController).Assembly);
            return builder;
        }

        public static IdentityBuilder AddCustomerIdentity(this IServiceCollection services, Action<IServiceCollection> action)
        {
            action(services);
            services.AddAuthorization(options => options.AddPolicy("", policy => policy.Requirements.Add(new PermissionRequirement())));
            IdentityBuilder identityBuilder = services.AddIdentity<IdentityUser, IdentityRole>(options => { options.SignIn.RequireConfirmedPhoneNumber = true; })
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>();
            return identityBuilder;
        }
    }
}
using Microsoft.AspNetCore.Authorization;

namespace FluentTest.Identity.Policies;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionRequirementHandler()
    {
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        //context.Resource
        throw new NotImplementedException();
    }
}

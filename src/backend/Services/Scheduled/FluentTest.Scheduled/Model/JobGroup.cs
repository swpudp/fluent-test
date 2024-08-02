using Dapper.Extensions.Expression;
using FluentTest.Infrastructure;

namespace FluentTest.Scheduled.Model;

[TableNaming(NamingPolicy.SnakeCase), FieldNaming(NamingPolicy.SnakeCase)]
public class JobGroup : FullBaseEntity<string>
{
    public string Name { get; set; }
}

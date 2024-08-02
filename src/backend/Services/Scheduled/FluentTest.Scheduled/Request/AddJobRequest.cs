using FluentTest.Scheduled.EnumCollection;

namespace FluentTest.Scheduled.Request;

public class AddJobRequest
{
    public string JobName { get;set; }

    public string JobGroup { get;set; }

    public JobType JobType { get;set; }

    public string Descrption {  get;set; }

    public string Cron {  get;set; }

    public Dictionary<string, string> JobData { get; set; }
}

using Quartz;

namespace FluentTest.Scheduled.Request;

public class RescheduleRequest
{
    public string JobName { get;set; }

    public string JobGroup { get;set; }

    public string Cron {  get;set; }

    public TriggerState JobStatus { get; set; }
}

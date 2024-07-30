using Quartz;

namespace FluentTest.Scheduled.Response
{
    public class JobView
    {
        public string JobType { get; set; }
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string JobClass { get; set; }
        public string Description { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string Cron { get; set; }
        public DateTimeOffset? LastFire { get; set; }
        public DateTimeOffset? NextFire { get; set; }
        public TriggerState JobStatus { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset? RestartTime { get; set; }
        public int Count { get; set; }
        public JobDataMap JobDataMap { get; set; }
    }
}

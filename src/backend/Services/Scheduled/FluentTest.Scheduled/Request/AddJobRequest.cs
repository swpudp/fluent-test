using FluentTest.Scheduled.EnumCollection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Scheduled.Request
{
    public class AddJobRequest
    {
        public string JobName { get;set; }

        public string JobGroup { get;set; }

        public JobType JobType { get;set; }

        public string Descrption {  get;set; }

        public string Cron {  get;set; }

        public Dictionary<string, string> JobData { get; set; }

        public TriggerState JobStatus { get; set; }
    }
}

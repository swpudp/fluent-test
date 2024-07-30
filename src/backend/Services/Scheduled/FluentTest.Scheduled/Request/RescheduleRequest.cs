using FluentTest.Scheduled.EnumCollection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Scheduled.Request
{
    public class RescheduleRequest
    {
        public string JobName { get;set; }

        public string JobGroup { get;set; }

        public string Cron {  get;set; }

        public TriggerState JobStatus { get; set; }
    }
}

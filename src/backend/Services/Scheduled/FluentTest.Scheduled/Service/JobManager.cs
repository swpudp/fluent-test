using FluentTest.Infrastructure;
using FluentTest.Scheduled.EnumCollection;
using FluentTest.Scheduled.Jobs;
using FluentTest.Scheduled.Request;
using FluentTest.Scheduled.Response;
using Quartz;
using Quartz.Impl.Matchers;

namespace FluentTest.Scheduled.Service
{
    public class JobManager(IScheduler scheduler)
    {
        private readonly IScheduler _scheduler = scheduler;

        public IDictionary<JobType, Func<AddJobRequest, IJobDetail>> JobDelegates { get; } = new Dictionary<JobType, Func<AddJobRequest, IJobDetail>>
        {
            [JobType.Http] = CreateHttpJob,
            [JobType.Mediat] = CreateMediatJob
        };

        public async Task AddJob(AddJobRequest request)
        {
            bool hasJob = await _scheduler.CheckExists(JobKey.Create(request.JobName, request.JobGroup));
            if (hasJob)
            {
                throw new BusinessExpcetion("任务已存在");
            }
            if (!JobDelegates.TryGetValue(request.JobType, out Func<AddJobRequest, IJobDetail>? func))
            {
                throw new BusinessExpcetion("不支持的类型");
            }
            IJobDetail jobDetail = func(request);
            AddJobData(request.JobData, jobDetail);

            IScheduleBuilder cronBuilder = CreateCronScheduleBuilder(request.Cron);
            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(request.JobName, request.JobGroup)
                .WithIdentity(request.JobName, request.JobGroup)
                .WithSchedule(cronBuilder)
                .Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public async Task Reschedule(RescheduleRequest request)
        {
            IScheduleBuilder cronBuilder = CreateCronScheduleBuilder(request.Cron);
            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(request.JobName, request.JobGroup)
                .WithIdentity(request.JobName, request.JobGroup)
                .WithSchedule(cronBuilder)
                .Build();
            await _scheduler.RescheduleJob(new TriggerKey(request.JobName, request.JobGroup), trigger);
            if (request.JobStatus == TriggerState.Paused)
            {
                await scheduler.PauseJob(trigger.JobKey);
            }
        }

        public async Task<List<JobView>> ListJobsAsync()
        {
            IReadOnlyCollection<string> groups = await _scheduler.GetJobGroupNames();
            List<JobView> jobs = new List<JobView>();
            foreach (string item in groups)
            {
                await ListGroupJobs(jobs, item);
            }
            return jobs;
        }

        public async Task<JobView> GetJobAsync(string name, string group)
        {
            JobKey jobKey = JobKey.Create(name, group);
            IJobDetail jobDetail = await _scheduler.GetJobDetail(jobKey);
            if (jobDetail == null)
            {
                return null;
            }
            var triggers = await scheduler.GetTriggersOfJob(jobKey);
            return await CreateJob(jobDetail, triggers.ElementAt(0));
        }

        public async Task PauseJob(string name, string group)
        {
            await _scheduler.PauseJob(JobKey.Create(name, group));
        }

        public async Task ResumeJob(string name, string group)
        {
            await _scheduler.ResumeJob(JobKey.Create(name, group));
        }

        public async Task RemoveJob(string name, string group)
        {
            await _scheduler.DeleteJob(JobKey.Create(name, group));
        }

        public async Task PauseGroupJobs(string groupName)
        {
            await _scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(groupName));
        }

        public async Task ResumeGroupJobs(string groupName)
        {
            await _scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(groupName));
        }

        public async Task PauseAllAsync()
        {
            await _scheduler.PauseAll();
        }

        public async Task ResumeAllAsync()
        {
            await _scheduler.ResumeAll();
        }

        private async Task ListGroupJobs(List<JobView> jobs, string groupName)
        {
            IReadOnlyCollection<JobKey> jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));
            foreach (JobKey item in jobKeys)
            {
                IJobDetail jobDetail = await _scheduler.GetJobDetail(item);
                if (jobDetail == null)
                {
                    continue;
                }
                IReadOnlyCollection<ITrigger> triggers = await _scheduler.GetTriggersOfJob(item);
                foreach (ITrigger trigger in triggers)
                {
                    JobView job = await CreateJob(jobDetail, trigger);
                    jobs.Add(job);
                }
            }
        }

        private async Task<JobView> CreateJob(IJobDetail jobDetail, ITrigger trigger)
        {
            JobView job = new JobView
            {
                JobGroup = jobDetail.Key.Group,
                JobName = jobDetail.Key.Name,
                JobClass = jobDetail.GetType().Name,
                Description = jobDetail.Description,
                JobDataMap = jobDetail.JobDataMap,
            };
            await CreateTrigger(job, trigger);
            return job;
        }

        private async Task CreateTrigger(JobView job, ITrigger trigger)
        {
            job.TriggerGroup = trigger.Key.Group;
            job.TriggerName = trigger.Key.Name;
            //todo 表达式获取
            //job.Cron=
            job.LastFire = trigger.GetPreviousFireTimeUtc();
            job.NextFire = trigger.GetNextFireTimeUtc();
            job.JobStatus = await _scheduler.GetTriggerState(trigger.Key);
            job.StartTime = trigger.StartTimeUtc;
            job.EndTime = trigger.EndTimeUtc;
        }

        private static void AddJobData(Dictionary<string, string> jobData, IJobDetail jobDetail)
        {
            if (jobData == null || jobData.Count == 0)
            {
                return;
            }
            foreach (KeyValuePair<string, string> item in jobData)
            {
                jobDetail.JobDataMap.Put(item.Key, item.Value);
            }
        }

        private static IScheduleBuilder CreateCronScheduleBuilder(string cron)
        {
            if (!CronExpression.IsValidExpression(cron))
            {
                throw new BusinessExpcetion("cron表达式不正确");
            }
            return CronScheduleBuilder.CronSchedule(cron)
                    .WithMisfireHandlingInstructionDoNothing()
                    .InTimeZone(TimeZoneInfo.Local);
        }

        private static IJobDetail CreateHttpJob(AddJobRequest request)
        {
            return JobBuilder.Create<HttpJob>()
                    .WithIdentity(JobKey.Create(request.JobName, request.JobGroup))
                    .WithDescription(request.Descrption)
                    .Build();
        }

        private static IJobDetail CreateMediatJob(AddJobRequest request)
        {
            return JobBuilder.Create<MediatJob>()
                    .WithIdentity(JobKey.Create(request.JobName, request.JobGroup))
                    .WithDescription(request.Descrption)
                    .Build();
        }
    }
}

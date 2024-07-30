using FluentTest.Scheduled.Request;
using FluentTest.Scheduled.Response;
using FluentTest.Scheduled.Service;
using Microsoft.AspNetCore.Mvc;

namespace FluentTest.Scheduled.Application
{
    [ApiController]
    [Route("[controller]")]
    public class JobManagerController(JobManager jobManager) : ControllerBase
    {
        private readonly JobManager _jobManager = jobManager;

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        [HttpGet("jobs")]
        public async Task<List<JobView>> ListJobsAsync()
        {
            return await _jobManager.ListJobsAsync();
        }

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="request">创建任务请求</param>
        /// <returns></returns>
        [HttpPost("newJob")]
        public async Task CreateAsync([FromBody] AddJobRequest request)
        {
            await _jobManager.AddJob(request);
        }

        /// <summary>
        /// 获取一个任务
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="group">任务组别</param>
        /// <returns>任务详细</returns>
        [HttpGet("jobDetail")]
        public async Task<JobView> GetJobViewAsync(string name, string group)
        {
            return await _jobManager.GetJobAsync(name, group);
        }

        /// <summary>
        /// 暂停一个任务
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="group">任务组别</param>
        /// <returns></returns>
        [HttpGet("pause")]
        public async Task PauseJobAsync(string name, string group)
        {
            await _jobManager.PauseJob(name, group);
        }

        /// <summary>
        /// 恢复一个任务
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="group">任务组别</param>
        /// <returns></returns>
        [HttpGet("resume")]
        public async Task ResumeJobAsync(string name, string group)
        {
            await _jobManager.ResumeJob(name, group);
        }

        /// <summary>
        /// 重新调度
        /// </summary>
        /// <param name="request">重新调度请求</param>
        /// <returns></returns>
        [HttpPost("reschedule")]
        public async Task Reschedule([FromBody] RescheduleRequest request)
        {
            await _jobManager.Reschedule(request);
        }

        /// <summary>
        /// 恢复一个任务
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="group">任务组别</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task RemoveJobAsync(string name, string group)
        {
            await _jobManager.RemoveJob(name, group);
        }

        /// <summary>
        /// 暂停一个组的所有任务
        /// </summary>
        /// <param name="group">任务组别</param>
        /// <returns></returns>
        [HttpGet("group/pause")]
        public async Task PauseGroupJobsAsync(string group)
        {
            await _jobManager.PauseGroupJobs(group);
        }

        /// <summary>
        /// 恢复一个组的所有任务
        /// </summary>
        /// <param name="group">任务组别</param>
        /// <returns></returns>
        [HttpGet("group/resume")]
        public async Task ResumeGroupJobsAsync(string group)
        {
            await _jobManager.ResumeGroupJobs(group);
        }

        /// <summary>
        /// 暂停所有任务
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/pause")]
        public async Task PauseAllAsync()
        {
            await _jobManager.PauseAllAsync();
        }

        /// <summary>
        /// 恢复所有任务
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/resume")]
        public async Task ResumeAllAsync()
        {
            await _jobManager.ResumeAllAsync();
        }
    }
}

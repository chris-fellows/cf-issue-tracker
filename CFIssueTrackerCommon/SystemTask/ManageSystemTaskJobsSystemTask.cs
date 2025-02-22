using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Manage system task jobs. Deletes old jobs that are completed. We keep them around for a while so that an issues
    /// can be investigated.
    /// </summary>
    public class ManageSystemTaskJobsSystemTask : ISystemTask
    {
        public static string TaskName => "Manage System Task Jobs";

        public string Name => TaskName;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {            
            var systemTaskJobService = serviceProvider.GetRequiredService<ISystemTaskJobService>();
            var systemTaskStatusService = serviceProvider.GetRequiredService<ISystemTaskStatusService>();            
            
            // Get system task statuses
            var systemTaskStatusCompletedSuccess = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.CompletedSuccess);
            var systemTaskStatusCompletedError = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.CompletedError);
            
            // Get old system task jobs that can be deleted
            var systemTaskJobFilter = new SystemTaskJobFilter()
            {
                CreatedDateTimeTo = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromHours(72)),
                StatusIds = new List<string>() 
                {
                    //systemTaskStatusActive.Id,
                    systemTaskStatusCompletedSuccess.Id, 
                    systemTaskStatusCompletedError.Id 
                },                
            };
            var systemTaskJobs = (await systemTaskJobService.GetByFilterAsync(systemTaskJobFilter)).OrderBy(s => s.CreatedDateTime).ToList();

            // Delete jobs
            while (systemTaskJobs.Any() && !cancellationToken.IsCancellationRequested)
            {
                var systemTaskJob = systemTaskJobs.First();
                await systemTaskJobService.DeleteByIdAsync(systemTaskJob.Id);
                systemTaskJobs.Remove(systemTaskJob);              
            }
        }
    }
}

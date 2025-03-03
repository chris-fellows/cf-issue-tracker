using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.SystemTask;
using CFIssueTracker.Utilities;

namespace CFIssueTracker.SystemTask
{
    /// <summary>
    /// Cleans up temp files:
    /// - Deletes old images uploaded. E.g. User image
    /// </summary>
    public class TempFileCleanupSystemTask : ISystemTask
    {
        public string Name => SystemTaskTypeNames.TempFilesCleanup;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {

            await DeleteTempImagesAsync(ConfigUtilities.ImageTempFilesRootFolder, TimeSpan.FromMinutes(30));            
        }

        /// <summary>
        /// Delete images uploaded
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        private async Task DeleteTempImagesAsync(string folder, TimeSpan maxAge)
        {
            if (Directory.Exists(folder))
            {
                foreach(var subFolder in Directory.GetDirectories(folder))
                {
                    await DeleteOldFilesAsync(subFolder, maxAge);
                }             
            }
        }

        private async Task DeleteOldFilesAsync(string folder, TimeSpan maxAge)
        {
            if (Directory.Exists(folder))
            {
                var expiryTime = DateTimeOffset.UtcNow.Subtract(maxAge);

                foreach (var file in Directory.GetFiles(folder))
                {
                    if (File.GetLastWriteTimeUtc(file) <= expiryTime)
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}

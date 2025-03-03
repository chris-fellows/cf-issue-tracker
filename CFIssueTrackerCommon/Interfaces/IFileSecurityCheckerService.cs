namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Checks files for security. E.g. Ensure that only images are uploaded.
    /// </summary>
    public interface IFileSecurityCheckerService
    {
        /// <summary>
        /// Validates that file can be uploaded as an image
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<bool> ValidateCanUploadImageAsync(byte[] content);
    }
}

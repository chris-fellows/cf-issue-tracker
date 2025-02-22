namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Returns email content
    /// </summary>
    public interface IEmailContent
    {
        string Name { get; }

        string GetBody(Dictionary<string, string> parameters);
    }
}

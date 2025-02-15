namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Cache
    /// </summary>
    public interface ICache
    {
        void Clear();

        void Add<T>(T item, string key, TimeSpan expiry);

        T? Get<T>(string key);

        void Remove(string key);
    }
}

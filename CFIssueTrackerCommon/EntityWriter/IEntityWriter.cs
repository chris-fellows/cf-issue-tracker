namespace CFIssueTrackerCommon.EntityWriter
{
    /// <summary>
    /// Writes entity to some format (E.g. CSV file, JSON file, XML file etc)
    /// </summary>
    /// <typeparam name="TEntityType">Type of entity to write</typeparam>
    public interface IEntityWriter<TEntityType>
    {
        void Write(IEnumerable<TEntityType> entities);
    }
}

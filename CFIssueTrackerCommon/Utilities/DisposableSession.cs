namespace CFIssueTrackerCommon.Utilities
{
    /// <summary>
    /// Session that performs one or more actions when disposed
    /// </summary>
    public class DisposableSession : IDisposable
    {
        private List<Action> _actions = new List<Action>();

        public DisposableSession()
        {

        }

        public void Add(Action action) => _actions.Add(action);

        public void Dispose()
        {
            var exceptions = new List<Exception>();

            foreach(var action in _actions)
            {
                try
                {
                    action();
                }
                catch(Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count == 1)
            {
                throw exceptions[0];
            }
            else if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }           
    }
}

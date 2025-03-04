using CFIssueTrackerCommon.Data;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    /// <summary>
    /// Base for EF service
    /// </summary>
    public abstract class EFBaseService : IDisposable
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;
        private CFIssueTrackerContext? _context;
        //private readonly Lazy<CFIssueTrackerContext> _contextLazy;

        public EFBaseService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
            //_contextLazy = new Lazy<CFIssueTrackerContext>(() =>
            //{
            //    return _dbFactory.CreateDbContext();
            //});
        }

        /// <summary>
        /// DB context. Creates if instance not set.
        /// </summary>
        protected CFIssueTrackerContext Context
        {
            get
            {
                lock (_dbFactory)
                {
                    if (_context == null) _context = _dbFactory.CreateDbContext();
                    return _context;
                }
            }
        }
   
        public void Dispose()
        {            
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

            //if (_contextLazy
            //{
            //    _contextLazy.Value.Dispose();
            //}            
        }
    }
}

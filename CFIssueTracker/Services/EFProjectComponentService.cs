//using CFIssueTrackerCommon.Interfaces;
//using CFIssueTrackerCommon.Models;
//using Microsoft.EntityFrameworkCore;

//namespace CFIssueTrackerCommon.Services
//{
//    public class EFProjectComponentService : IProjectComponentService
//    {
//        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

//        public EFProjectComponentService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
//        {
//            _dbFactory = dbFactory;
//        }

//        public async Task<List<ProjectComponent>> GetAllAsync()
//        {
//            using (var context = _dbFactory.CreateDbContext())
//            {
//                var projectComponents = context.ProjectComponent;
//                return await projectComponents.ToListAsync();
//            }
//        }
//    }
//}

using CFIssueTracker.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueTypeService : IIssueTypeService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFIssueTypeService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<IssueType>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueTypes = context.IssueType;
                return await issueTypes.ToListAsync();
            }                
        }

        public async Task<IssueType> AddAsync(IssueType issueType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.IssueType.Add(issueType);
                await context.SaveChangesAsync();
                return issueType;
            }
        }
        public async Task<IssueType?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueType = await context.IssueType.FirstOrDefaultAsync(i => i.Id == id);
                return issueType;
            }
        }
    }
}

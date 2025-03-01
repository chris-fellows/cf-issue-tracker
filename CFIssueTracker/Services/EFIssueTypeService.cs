﻿using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueTypeService : IIssueTypeService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFIssueTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<IssueType> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.IssueType.OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<List<IssueType>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.IssueType.ToListAsync()).OrderBy(i => i.Name).ToList();
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

        public async Task<IssueType> UpdateAsync(IssueType issueType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.IssueType.Update(issueType);
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

        public async Task<List<IssueType>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.IssueType.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueType = await context.IssueType.FirstOrDefaultAsync(i => i.Id == id);
                if (issueType != null)
                {
                    context.IssueType.Remove(issueType);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<IssueType?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueType = await context.IssueType.FirstOrDefaultAsync(i => i.Name == name);
                return issueType;
            }
        }
    }
}

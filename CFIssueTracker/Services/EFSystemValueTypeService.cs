using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
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
    public class EFSystemValueTypeService : ISystemValueTypeService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFSystemValueTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<SystemValueType> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.SystemValueType.OrderBy(u => u.Name).ToList();
            }
        }

        public async Task<List<SystemValueType>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.SystemValueType.ToListAsync()).OrderBy(u => u.Name).ToList();
            }
        }

        public async Task<SystemValueType> AddAsync(SystemValueType systemValueType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemValueType.Add(systemValueType);
                await context.SaveChangesAsync();
                return systemValueType;
            }
        }

        public async Task<SystemValueType> UpdateAsync(SystemValueType systemValueType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemValueType.Update(systemValueType);
                await context.SaveChangesAsync();
                return systemValueType;
            }
        }

        public async Task<SystemValueType?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemValueType = await context.SystemValueType.FirstOrDefaultAsync(i => i.Id == id);
                return systemValueType;
            }
        }

        public async Task<List<SystemValueType>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.SystemValueType.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public SystemValueType? GetById(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemValueType = context.SystemValueType.FirstOrDefault(i => i.Id == id);
                return systemValueType;
            }
        }
    }
}

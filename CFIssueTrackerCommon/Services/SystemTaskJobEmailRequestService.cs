//using CFIssueTrackerCommon.Constants;
//using CFIssueTrackerCommon.Interfaces;
//using CFIssueTrackerCommon.Models;

//namespace CFIssueTrackerCommon.Services
//{
//    /// <summary>
//    /// Handles requests to send emails via system task job
//    /// </summary>
//    public class SystemTaskJobEmailRequestService : IEmailRequestService
//    {
//        private readonly ISystemTaskJobService _systemTaskJobService;
//        private readonly ISystemTaskStatusService _systemTaskStatusService;
//        private readonly ISystemTaskTypeService _systemTaskTypeService;
//        private readonly ISystemValueTypeService _systemValueTypeService;

//        public SystemTaskJobEmailRequestService(ISystemTaskJobService systemTaskJobService,
//                    ISystemTaskStatusService systemTaskStatusService,
//                    ISystemTaskTypeService systemTaskTypeService,
//                    ISystemValueTypeService systemValueTypeService)
//        {
//            _systemTaskJobService = systemTaskJobService;
//            _systemTaskStatusService = systemTaskStatusService;
//            _systemTaskTypeService = systemTaskTypeService; 
//            _systemValueTypeService = systemValueTypeService;
//        }

//        public async Task AddIssueAssignedAsync(Issue issue)
//        {
//            var systemTaskStatus = await _systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Pending);
//            var systemTaskType = await _systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendIssueAssignedEmail);
//            var systemValueTypePasswordIssueId = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.IssueId);

//            var systemTaskJob = new SystemTaskJob()
//            {
//                Id = Guid.NewGuid().ToString(),
//                CreatedDateTime = DateTimeOffset.UtcNow,
//                StatusId = systemTaskStatus.Id,
//                TypeId = systemTaskType.Id,
//                Parameters = new List<SystemTaskParameter>()
//                {
//                    new SystemTaskParameter()
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        SystemValueTypeId = systemValueTypePasswordIssueId.Id,
//                        Value = issue.Id
//                    }
//                }
//            };

//            await _systemTaskJobService.AddAsync(systemTaskJob);
//        }

//        public async Task AddResetPasswordAsync(PasswordReset passwordReset, User user)
//        {
//            var systemTaskStatus = await _systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Pending);
//            var systemTaskType = await _systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendResetPasswordEmail);
//            var systemValueTypePasswordResetId = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.PasswordResetId);

//            var systemTaskJob = new SystemTaskJob()
//            {
//                Id = Guid.NewGuid().ToString(),
//                CreatedDateTime = DateTimeOffset.UtcNow,
//                StatusId = systemTaskStatus.Id,
//                TypeId = systemTaskType.Id,
//                Parameters = new List<SystemTaskParameter>()
//                {
//                    new SystemTaskParameter()
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        SystemValueTypeId = systemValueTypePasswordResetId.Id,
//                        Value = passwordReset.Id
//                    }
//                }
//            };

//            await _systemTaskJobService.AddAsync(systemTaskJob);
//        }

//        public async Task AddNewUserAsync(User user)
//        {
//            var systemTaskStatus = await _systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Pending);
//            var systemTaskType = await _systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendNewUserEmail);
//            var systemValueTypeUserId = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.UserId);

//            var systemTaskJob = new SystemTaskJob()
//            {
//                Id = Guid.NewGuid().ToString(),
//                CreatedDateTime = DateTimeOffset.UtcNow,
//                StatusId = systemTaskStatus.Id,
//                TypeId = systemTaskType.Id,
//                Parameters = new List<SystemTaskParameter>()
//                {
//                    new SystemTaskParameter()
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        SystemValueTypeId = systemValueTypeUserId.Id,
//                        Value = user.Id
//                    }
//                }
//            };

//            await _systemTaskJobService.AddAsync(systemTaskJob);
//        }
//    }
//}

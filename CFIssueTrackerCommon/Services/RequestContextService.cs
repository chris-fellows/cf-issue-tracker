﻿using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CFIssueTrackerCommon.Services
{
    public class RequestContextService : IRequestContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public RequestContextService(IHttpContextAccessor httpContextAccessor,
                                     IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public string? UserId
        {            
            get
            {                
                var claimsIdentity = GetClaimsIdentity();
                if (claimsIdentity != null)
                {
                    var nameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (nameClaim != null)
                    {
                        return nameClaim.Value;
                    }
                }
                return null;
            }
        }

        public User? User
        {
            get
            {
                var claimsIdentity = GetClaimsIdentity();
                if (claimsIdentity != null)
                { 
                    var nameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);                    
                    if (nameClaim != null)
                    {
                        // TODO: Remove this when we store UserId in claims
                        if (nameClaim.Value.Contains("@"))   // Email
                        {
                            return _userService.GetAll().FirstOrDefault(u => u.Email == nameClaim.Value);                            
                        }

                        var user = _userService.GetById(nameClaim.Value);
                        return user;
                    }
                }
                
                return null;
            }
        }

        private ClaimsIdentity? GetClaimsIdentity()
        {
            if (_httpContextAccessor.HttpContext != null &&
                   _httpContextAccessor.HttpContext.User.Identity != null &&
                   _httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity)
            {
                return (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            }
            return null;
        }
    }
}

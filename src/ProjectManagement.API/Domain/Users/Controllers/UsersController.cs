using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Domain.Projects.Services;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Domain.Users.Models;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Domain.Users.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        
        private readonly IUsersAppService _usersAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UsersController(IUsersAppService usersAppService, UserManager<ApplicationUser> userManager)
        {
            _usersAppService = usersAppService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var users = await _usersAppService.GetUsersAsync(user);
            return Ok(users);
        }
    }
    
    
}
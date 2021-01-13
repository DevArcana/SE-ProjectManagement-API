using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Domain.Projects.Services;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Domain.Users.Models;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Domain.Users.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectsService> _logger;
        private readonly IMapper _mapper;
        
        public UsersController(ApplicationDbContext context, ILogger<ProjectsService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _context.Users.AsNoTracking().AsQueryable().ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }
    }
    
    
}
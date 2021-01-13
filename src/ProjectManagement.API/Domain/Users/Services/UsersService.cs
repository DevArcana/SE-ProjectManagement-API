using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Domain.Users.Interfaces;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Domain.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        
        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IQueryable<ApplicationUser> GetAllUsersAsync(ApplicationUser user)
        {
            return _context.Users.AsNoTracking().Where(x=> x.Id!=user.Id);
        }
    }
}
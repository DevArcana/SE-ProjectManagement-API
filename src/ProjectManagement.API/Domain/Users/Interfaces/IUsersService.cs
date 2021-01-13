using System.Linq;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Domain.Users.Interfaces
{
    public interface IUsersService
    {
        IQueryable<ApplicationUser> GetAllUsersAsync(ApplicationUser user);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Domain.Users.Models;

namespace ProjectManagement.API.Application.Interfaces
{
    public interface IUsersAppService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    }
}
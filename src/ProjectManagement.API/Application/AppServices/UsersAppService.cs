using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Domain.Users.Interfaces;
using ProjectManagement.API.Domain.Users.Models;

namespace ProjectManagement.API.Application.AppServices
{
    public class UsersAppService : IUsersAppService
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        
        public UsersAppService(IMapper mapper, IUsersService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }
        
        public async Task<IEnumerable<UserDto>> GetUsersAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            return await _usersService.GetAllUsersAsync(user).ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
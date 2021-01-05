using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Domain.Issues.Interfaces;
using ProjectManagement.API.Domain.Issues.Model;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Application.AppServices
{
    public class IssuesAppService : IIssuesAppService
    {
        private readonly ILogger<IssuesAppService> _logger;
        private readonly IMapper _mapper;
        private readonly IIssuesService _issuesService;

        public IssuesAppService(ILogger<IssuesAppService> logger, IMapper mapper, IIssuesService issuesService)
        {
            _logger = logger;
            _mapper = mapper;
            _issuesService = issuesService;
        }

        public async Task<IssueDto> CreateIssueAsync(ApplicationUser user, long projectId, string name, string description, CancellationToken cancellationToken = default)
        {
            var issue = await _issuesService.CreateIssueAsync(user, projectId, name, description, cancellationToken);
            return issue == null ? null : _mapper.Map<IssueDto>(issue);
        }

        public async Task<IssueDto> GetIssueByIdAsync(ApplicationUser user, long issueId, CancellationToken cancellationToken = default)
        {
            var issue = await _issuesService.GetIssueByIdAsync(user, issueId, cancellationToken);
            return issue == null ? null : _mapper.Map<IssueDto>(issue);
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesForProjectAsync(ApplicationUser user, long projectId, CancellationToken cancellationToken = default)
        {
            var issues = await _issuesService.GetIssuesForProjectAsync(user, projectId, cancellationToken);
            return await issues.ProjectTo<IssueDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
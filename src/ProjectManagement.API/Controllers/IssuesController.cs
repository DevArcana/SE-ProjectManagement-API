using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Application.Interfaces;
using ProjectManagement.API.Application.Models;
using ProjectManagement.API.Domain.Users.Entities;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/projects/{projectId}/[controller]")]
    public class IssuesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIssuesAppService _issuesAppService;

        public IssuesController(UserManager<ApplicationUser> userManager, IIssuesAppService issuesAppService)
        {
            _userManager = userManager;
            _issuesAppService = issuesAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(long projectId, [FromBody] CreateIssueDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var issue = await _issuesAppService.CreateIssueAsync(user, projectId, dto.Name, dto.Description);

            if (issue == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Project {projectId} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            
            return CreatedAtAction(nameof(GetIssueById), new {ProjectId = projectId, IssueId = issue.Id}, issue);
        }
        
        [HttpGet("{issueId}")]
        public async Task<IActionResult> GetIssueById(long projectId, long issueId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var issue = await _issuesAppService.GetIssueByIdAsync(user, issueId);

            if (issue == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Issue {issueId} from project {projectId} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            
            return Ok(issue);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetIssues(long projectId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var issues = await _issuesAppService.GetIssuesForProjectAsync(user, projectId);

            if (issues == null)
            {
                var problem = new ProblemDetails
                {
                    Instance = HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Type = $"https://httpstatuses.com/404",
                    Title = "Not found",
                    Detail = $"Project {projectId} does not exist or you do not have access to it."
                };

                return NotFound(problem);
            }
            
            return Ok(issues);
        }
    }
}
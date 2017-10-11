using Microsoft.AspNetCore.Mvc;
using CtsContestWeb.Communication;
using CtsContestWeb.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CtsContestWeb.Filters;
using CtsContestWeb.Logic;

namespace CtsContestWeb.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskManager _taskManager;
        private readonly ICompiler _compiler;
        private readonly ISolutionLogic _solutionLogic;

        public TaskController(ITaskManager taskManager, ICompiler compiler, ISolutionLogic solutionLogic)
        {
            _taskManager = taskManager;
            _compiler = compiler;
            _solutionLogic = solutionLogic;
        }

        public async Task<IEnumerable<TaskDisplayDto>> Get()
        {
            string userEmail = null;
            if (User.Identity.IsAuthenticated)
                userEmail = User.FindFirst(ClaimTypes.Email).Value;

            var tasks = await _taskManager.GetAllTasks(userEmail);
            return tasks.Select(t => new TaskDisplayDto
            {
                Id = t.Id,
                Name = t.Name,
                Value = t.Value,
                IsSolved = t.IsSolved
            });
        }

        [HttpGet("{id}")]
        public async Task<TaskDisplayDto> Get(int id)
        {
            string userEmail = null;
            if (User.Identity.IsAuthenticated) 
                userEmail = User.FindFirst(ClaimTypes.Email).Value;

            var task = await _taskManager.GetTaskById(id, userEmail);

            return new TaskDisplayDto
            {
                Id = task.Id,
                Name = task.Name,
                Value = task.Value,
                Description = task.Description,
                IsSolved = task.IsSolved
            };
        }


        [LoggedIn]
        [HttpPut("[action]")]
        public async Task<CompileDto> Solve(int taskId, string source, int language)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            return await _solutionLogic.CheckSolution(taskId, source, language, userEmail);
        }

        [HttpGet("[action]")]
        public async Task<LanguageDto> GetLanguages()
        {
            return await _compiler.GetLanguages();
        }

        [HttpGet("[action]/{language}")]
        public async Task<CodeSkeletonDto> GetCodeSkeleton(string language)
        {
            return await _taskManager.GetCodeSkeleton(language);
        }
    }
}

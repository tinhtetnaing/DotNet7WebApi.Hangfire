using DotNet7WebApi.HangfireApp.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNet7WebApi.HangfireApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CronV1Controller : ControllerBase
    {
        private readonly AppDbContext _context;
        public CronV1Controller(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult BlogList()
        {
            var jobId = BackgroundJob.Schedule(
                () => Console.WriteLine("BlogList => {0}", JsonConvert.SerializeObject(_context.blogs.ToList(), Formatting.Indented)),
                TimeSpan.FromDays(1));
            var result = string.IsNullOrWhiteSpace(jobId) ? "Fail" : "Success";
            return Ok(result);
        }
    }
}

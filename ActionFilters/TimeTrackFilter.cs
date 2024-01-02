using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MeetupAPI.ActionFilters
{
    public class TimeTrackFilter(ILogger<TimeTrackFilter> logger) : IActionFilter
    {
        private readonly ILogger<TimeTrackFilter> _logger = logger;
        private Stopwatch _stopWatch;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopWatch.Stop();

            var milliseconds = _stopWatch.ElapsedMilliseconds;
            var action = context.ActionDescriptor.DisplayName;

            _logger.LogInformation($"Action: {action}, executed in {milliseconds} milliseconds.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }
    }
}

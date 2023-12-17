using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MeetupAPI.ActionFilters
{
    public class TimeTrackFilter : Attribute, IActionFilter
    {
        private Stopwatch _stopWatch;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopWatch.Stop();

            var milliseconds = _stopWatch.ElapsedMilliseconds;
            var action = context.ActionDescriptor.DisplayName;

            Debug.WriteLine($"Action: {action}, executed in {milliseconds} milliseconds.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }
    }
}

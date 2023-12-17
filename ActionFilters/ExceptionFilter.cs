using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MeetupAPI.ActionFilters
{
    public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger = logger;

        public void OnException(ExceptionContext context)
        {
            _logger.LogCritical($"Caught in ExceptionFilter {context.Exception.Message}", context.Exception);
            
            var result = new JsonResult("Something went wrong!");
            result.StatusCode = 500;

            context.Result = result;
        }
    }
}

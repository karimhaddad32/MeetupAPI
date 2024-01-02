using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MeetupAPI.Authorization
{
    public class MinimumAgeHandler(ILogger<MinimumAgeHandler> logger) : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeHandler> _logger = logger;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            bool dateProvided = DateTime.TryParse(context.User.FindFirst(c => c.Type == "DateOfBirth")?.Value, out DateTime dateOfBirth);

            _logger.LogInformation($"Handling minimum age requirement for: {userEmail}. [DateOfBirth: {dateOfBirth}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) > DateTime.Today || userEmail == null || !dateProvided) {

                _logger.LogInformation($"Access Denied!");
            }
            else
            {
                _logger.LogInformation($"Access Granted");
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

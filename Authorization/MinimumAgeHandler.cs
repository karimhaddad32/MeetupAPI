using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MeetupAPI.Authorization
{
    public class MinimumAgeHandler(ILogger<MinimumAgeHandler> logger) : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeHandler> _logger = logger;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            _logger.LogInformation($"Handling minimum age requirement for: {userEmail}. [DateOfBirth: {dateOfBirth}]");

            if(dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today) {

                _logger.LogInformation($"Access Granted");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation($"Access Denied!");
            }

            return Task.CompletedTask;
        }
    }
}

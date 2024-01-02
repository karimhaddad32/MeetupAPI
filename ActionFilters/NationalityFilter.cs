using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace MeetupAPI.ActionFilters
{
    public class NationalityFilter : Attribute, IAuthorizationFilter
    {
        private string[] _nationalities;
        public NationalityFilter(string nationalities) {
            _nationalities = nationalities.Split(",");
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var nationality = context.HttpContext.User.FindFirst(c => c.Type == "Nationality").Value;

            if (!_nationalities.Any(x => x == nationality)) {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}

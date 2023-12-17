using Microsoft.AspNetCore.Authorization;

namespace MeetupAPI.Authorization
{
    public class MinimumAgeRequirement :  IAuthorizationRequirement
    {

        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}

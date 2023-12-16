using MeetupAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace MeetupAPI.Identity
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(User user);
    } 
}

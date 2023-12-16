using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Identity;
using MeetupAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Controllers
{
    [Route("api/account")]
    public class AccountController(IMeetupRepository repository, IPasswordHasher<User> passwordHasher, IJwtProvider jwtProvider) : ControllerBase
    {
        private readonly IMeetupRepository _repository = repository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = new User
            {
                Email = registerUserDto.Email,
                Nationality = registerUserDto.Nationality,
                DateOfBirth = registerUserDto.DateOfBirth
            };

            var passwordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);
            user.PasswordHash = passwordHash;

            await _repository.AddNewUserAsync(user);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _repository.GetUserAsync(loginUserDto.Email);

            if(user == null)
                return BadRequest("Invalid username or password");

            var verified = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);

            if(verified == PasswordVerificationResult.Failed)
                return BadRequest("Invalid username or password");

            var token  = _jwtProvider.GenerateJwtToken(user);

            return Ok(token);
        }
    }
}

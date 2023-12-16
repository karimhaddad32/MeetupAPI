using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Controllers
{
    [Route("api/account")]
    public class AccountController(IMeetupRepository repository, IPasswordHasher<User> passwordHasher) : ControllerBase
    {
        private readonly IMeetupRepository _repository = repository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        [HttpPost]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = new User
            {
                Email = registerUserDto.Email,
                Nationality = registerUserDto.Nationality,
                DateOfBirth = registerUserDto.DateOfBirth,
            };

            var passwordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);
            user.PasswordHash = passwordHash;

            await _repository.AddNewUserAsync(user);

            return Ok();
        }
    }
}

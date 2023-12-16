using FluentValidation;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Repositories;

namespace MeetupAPI.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(IMeetupRepository meetupRepository) {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).Custom((value, context) => {
                var userAlreadyExists = meetupRepository.UserAlreadyExists(value);
                if (userAlreadyExists)
                {
                    context.AddFailure("Email", "That email address is taken");
                    return;
                }
            });
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
        }
    }
}

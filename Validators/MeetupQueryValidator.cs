using FluentValidation;
using MeetupAPI.Controllers;

namespace MeetupAPI.Validators
{
    public class MeetupQueryValidator : AbstractValidator<MeetupQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 15, 50 };
        public MeetupQueryValidator()
        {
            RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(q => q.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in {string.Join(", ", allowedPageSizes)}");
                }
            });
        }
    }
}

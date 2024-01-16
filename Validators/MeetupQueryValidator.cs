using FluentValidation;
using MeetupAPI.Controllers;
using MeetupAPI.Entities;

namespace MeetupAPI.Validators
{
    public class MeetupQueryValidator : AbstractValidator<MeetupQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 15, 50 };
        private string[] allowedSortByColumnsNames = new[] { 
            nameof(Meetup.Organizer), 
            nameof(Meetup.Date), 
            nameof(Meetup.Name) 
        };
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

            RuleFor(q => q.SortBy)
                .Must(value => string.IsNullOrWhiteSpace(value) || allowedSortByColumnsNames.Contains(value))
                .WithMessage($"SortBy is optional, or it has to be in ({string.Join(", ", allowedSortByColumnsNames)})");
        }
    }
}

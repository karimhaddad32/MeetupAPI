using MeetupAPI.Entities;

namespace MeetupAPI
{
    public static class MeetupData 
    {

        public static readonly List<Meetup> MockMeetUps =
        [
            new() {
                Id = 1,
                Name = "Tech Meetup",
                Organizer = "John Doe",
                Date = DateTime.Now.AddDays(7),
                IsPrivate = false,
            },
            new() {
                Id = 2,
                Name = "Data Science Forum",
                Organizer = "Alice Johnson",
                Date = DateTime.Now.AddDays(14),
                IsPrivate = true
            }
        ];

        public static readonly List<Location> Locations =
            [
                new Location
                {
                    MeetupId = 1,
                    Id = 1,
                    City = "Anytown",
                    Street = "123 Main St",
                    PostalCode = "12345"
                },
                new()
                {
                    MeetupId = 2,
                    Id = 2,
                    City = "Sciencetown",
                    Street = "456 Data St",
                    PostalCode = "54321"
                },
            ];

        public static readonly List<Lecture> Lectures =
            [
                new()
                {
                    MeetupId = 1,
                    Id = 1,
                    Author = "Jane Smith",
                    Topic = "Introduction to Programming",
                    Description = "A beginner-friendly session on programming basics.",
                },
                new()
                {
                    MeetupId = 1,
                    Id = 2,
                    Author = "Bob Johnson",
                    Topic = "Web Development Trends",
                    Description = "Exploring the latest trends in web development."
                },
                new()
                {
                    MeetupId = 2,
                    Id = 3,
                    Author = "Charlie Brown",
                    Topic = "Machine Learning Basics",
                    Description = "An introduction to the fundamentals of machine learning."
                },
                new()
                {
                    MeetupId = 2,
                    Id = 4,
                    Author = "Diana Davis",
                    Topic = "Big Data Analytics",
                    Description = "Analyzing large datasets for actionable insights."
                }
            ];


    }
}

using MeetupAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Repositories
{
    public interface IMeetupRepository
    {
        Task CreateNewMeetupAsync(Meetup meetupModel);
        Task<List<Meetup>> GetAllMeetupsAsync();
        Task<Meetup?> GetMeetupAsync(string name);
    }
}
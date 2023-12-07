using MeetupAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Repositories
{
    public interface IMeetupRepository
    {
        Task<List<Meetup>> GetAllMeetupsAsync();
    }
}
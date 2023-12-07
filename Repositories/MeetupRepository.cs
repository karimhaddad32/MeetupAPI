using MeetupAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Repositories
{
    public class MeetupRepository(MeetupContext dbContext) : IMeetupRepository
    {
        private readonly MeetupContext _dbContext = dbContext;

        public async Task<List<Meetup>> GetAllMeetupsAsync()
        {
            var meetups = await _dbContext.Meetups.AsNoTracking()
                .ToListAsync();

            return meetups;
        }
    }
}

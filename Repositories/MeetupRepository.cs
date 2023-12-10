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
                .Include(x => x.Location)
                .Include(x => x.Lectures)
                .ToListAsync();

            return meetups;
        }

        public async Task<Meetup?> GetMeetupAsync(string name)
        {
            var meetup = await _dbContext.Meetups.AsNoTracking()
                .Include(x => x.Location)
                .Include(x => x.Lectures)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            return meetup;
        }


        public async Task CreateNewMeetupAsync(Meetup meetupModel)
        {
            await _dbContext.Meetups.AddAsync(meetupModel);
            _dbContext.SaveChanges();
        }
    }
}

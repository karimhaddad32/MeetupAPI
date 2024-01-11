using AutoMapper.Features;
using MeetupAPI.Controllers;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace MeetupAPI.Repositories
{
    public class MeetupRepository(MeetupContext dbContext) : IMeetupRepository
    {
        private readonly MeetupContext _dbContext = dbContext;

        public async Task<(List<Meetup>, int)> GetAllMeetupsAsync(MeetupQuery query)
        {
            var baseQuery = _dbContext.Meetups.AsNoTracking()
                .Include(x => x.Location)
                .Include(x => x.Lectures)
                .Where(x => query.SearchPhrase == null ||
                            x.Organizer.ToLower().Contains(query.SearchPhrase, StringComparison.OrdinalIgnoreCase) ||
                            x.Name.ToLower().Contains(query.SearchPhrase, StringComparison.OrdinalIgnoreCase));

            var meetups = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var totalCount = baseQuery.Count();

            return (meetups, totalCount);
        }

        public async Task<List<Lecture>> GetLecturesAsync(string meetupName)
        {
            var meetup = await _dbContext.Meetups.AsNoTracking()
                .Include(x => x.Lectures)
                .SingleAsync(x=> x.Name.ToLower() == meetupName.ToLower());

            return meetup.Lectures.ToList();
        }

        public async Task<Meetup?> GetMeetupAsync(string name)
        {
            var meetup = await _dbContext.Meetups.AsNoTracking()
                .Include(x => x.Location)
                .Include(x => x.Lectures)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            return meetup;
        }

        public async Task<bool> MeetupAlreadyExistsAsync(string name)
        {
            return await _dbContext.Meetups.AsNoTracking()
                .AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task CreateNewMeetupAsync(Meetup meetupModel)
        {
            await _dbContext.Meetups.AddAsync(meetupModel);
            _dbContext.SaveChanges();
        }    
        
        public async Task DeleteLecturesAsync(string meetupName)
        {
            var model = await GetMeetupAsync(meetupName);

            _dbContext.Lectures.RemoveRange(model.Lectures);
            _dbContext.SaveChanges();
        } 

        public async Task DeleteLectureAsync(string meetupName, int lectureId)
        {
            var model = await GetMeetupAsync(meetupName);

            var lecutre = model.Lectures.SingleOrDefault(x => x.Id == lectureId);

            if (lecutre != null)
            {
                _dbContext.Lectures.Remove(lecutre);
                _dbContext.SaveChanges();
            }
        } 
        
        public async Task DeleteMeetupAsync(Meetup meetup)
        {
            _dbContext.Meetups.Remove(meetup);
            _dbContext.SaveChanges();
        }

        public async Task UpdateMeetupAsync(string name, Meetup newModel)
        {
            var oldModel = await GetMeetupAsync(name);

            if(oldModel == null) { return; }

            oldModel.Name = newModel.Name;
            oldModel.IsPrivate = newModel.IsPrivate;
            oldModel.Organizer = newModel.Organizer;
            oldModel.Date = newModel.Date;

            _dbContext.Update(oldModel);
            _dbContext.SaveChanges();
        }

        public async Task AddLectureAsync(string meetupName, Lecture lectureModel)
        {
            var model = await GetMeetupAsync(meetupName);

            model.Lectures.Add(lectureModel);

            _dbContext.Update(model);
            _dbContext.SaveChanges();
        }

        public async Task AddNewUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            _dbContext.SaveChanges();
        }   
        
        public bool UserAlreadyExists(string email)
        {
            return _dbContext.Users.Any(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await _dbContext.Users.AsNoTracking()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}

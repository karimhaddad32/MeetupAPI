using MeetupAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Repositories
{
    public interface IMeetupRepository
    {
        Task AddLectureAsync(string meetupName, Lecture lectureModel);
        Task AddNewUserAsync(User user);
        Task CreateNewMeetupAsync(Meetup meetupModel);
        Task DeleteLectureAsync(string meetupName, int lectureId);
        Task DeleteLecturesAsync(string meetupName);
        Task DeleteMeetupAsync(Meetup meetup);
        Task<List<Meetup>> GetAllMeetupsAsync();
        Task<List<Lecture>> GetLecturesAsync(string meetupName);
        Task<Meetup?> GetMeetupAsync(string name);
        Task<bool> MeetupAlreadyExistsAsync(string name);
        Task UpdateMeetupAsync(string name, Meetup newModel);
        bool UserAlreadyExists(string email);
        Task<User?> GetUserAsync(string email);
    }
}
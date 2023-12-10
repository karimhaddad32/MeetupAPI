﻿using MeetupAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Repositories
{
    public interface IMeetupRepository
    {
        Task AddLectureAsync(string meetupName, Lecture lectureModel);
        Task CreateNewMeetupAsync(Meetup meetupModel);
        Task DeleteLectureAsync(string meetupName, int lectureId);
        Task DeleteLecturesAsync(string meetupName);
        Task DeleteMeetupAsync(string name);
        Task<List<Meetup>> GetAllMeetupsAsync();
        Task<List<Lecture>> GetLecturesAsync(string meetupName);
        Task<Meetup?> GetMeetupAsync(string name);
        Task<bool> MeetupAlreadyExistsAsync(string name);
        Task UpdateMeetupAsync(string name, Meetup newModel);
    }
}
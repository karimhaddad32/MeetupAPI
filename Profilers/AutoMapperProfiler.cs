using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using System.Collections.Generic;

namespace MeetupAPI.Profilers
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<Location, LocationDto>()
                .ReverseMap();

            CreateMap<Lecture, LectureDto>()
                .ReverseMap();

            CreateMap<Meetup, FullMeetupDto>()
                .ReverseMap()
                .ForMember(model => model.CreatedBy, config => config.Ignore())
                .ForMember(model => model.CreatedById, config => config.Ignore());

            CreateMap<Meetup, MeetupDto>()
                .ReverseMap();

        }
    }
}

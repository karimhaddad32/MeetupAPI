using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Controllers
{
    [Route("api/meetup")]
    public class MeetupController(IMeetupRepository meetupRepository, IMapper mapper) : Controller
    {
        private readonly IMeetupRepository _meetupRepository = meetupRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<List<MeetupDto>>> Get()
        {
            var meetups = await _meetupRepository.GetAllMeetupsAsync();

            if (meetups == null) { return NotFound(meetups); }

            var meetupDtos = _mapper.Map<List<MeetupDto>>(meetups);

            return Ok(meetupDtos);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<MeetupDto>> Get(string name)
        {
            var meetup = await _meetupRepository.GetMeetupAsync(name);

            if (meetup == null) { return NotFound(meetup); }

            var meetupDto = _mapper.Map<MeetupDto>(meetup);

            return Ok(meetupDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MeetupDto meetupDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var meetupModel = _mapper.Map<Meetup>(meetupDto);

            await _meetupRepository.CreateNewMeetupAsync(meetupModel);

            return Ok();
        }
    }
}

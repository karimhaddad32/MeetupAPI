using AutoMapper;
using MeetupAPI.ActionFilters;
using MeetupAPI.Authorization;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetupAPI.Controllers
{
    [Route("api/meetup")]   
    [Authorize]
    [ServiceFilter<TimeTrackFilter>]
    public class MeetupController(IMeetupRepository meetupRepository, IMapper mapper, IAuthorizationService authorizationService) : Controller
    {
        private readonly IMeetupRepository _meetupRepository = meetupRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<FullMeetupDto>>> GetAll([FromQuery] MeetupQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (meetups, totalCount) = await _meetupRepository.GetAllMeetupsAsync(query);

            if (meetups == null) { return NotFound(meetups); }

            var meetupDtos = _mapper.Map<List<FullMeetupDto>>(meetups);

            var result = new PagedResult<FullMeetupDto>(meetupDtos, totalCount, query.PageNumber, query.PageSize);

            return Ok(result);
        }

        [HttpGet("{name}")]
        [Authorize(Policy = "HasNationality")]
        [Authorize(Policy = "AtLeast18")]
        [NationalityFilter("English,German")]
        public async Task<ActionResult<FullMeetupDto>> Get(string name)
        {
            var meetup = await _meetupRepository.GetMeetupAsync(name);

            if (meetup == null) { return NotFound(meetup); }

            var meetupDto = _mapper.Map<FullMeetupDto>(meetup);

            return Ok(meetupDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Post([FromBody] MeetupDto meetupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _meetupRepository.MeetupAlreadyExistsAsync(meetupDto.Name))
            {
                throw new ArgumentException("Meetup name is already taken");
            }

            var meetupModel = _mapper.Map<Meetup>(meetupDto);

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if(userId == null) { return Forbid(); }

            meetupModel.CreatedById = int.Parse(userId);

            await _meetupRepository.CreateNewMeetupAsync(meetupModel);

            return Created($"api/meetup/{meetupModel.Name}", null);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> Put(string name, [FromBody] FullMeetupDto meetupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _meetupRepository.MeetupAlreadyExistsAsync(name))
            {
                return NotFound(meetupDto);
            }

            if (await _meetupRepository.MeetupAlreadyExistsAsync(meetupDto.Name))
            {
                return BadRequest("Name is Already Taken");
            }

            var newModel = _mapper.Map<Meetup>(meetupDto);

            var authorizationResult = _authorizationService.AuthorizeAsync(User, newModel, new ResourceOperationRequirement(OperationType.Update)).Result;

            if(!authorizationResult.Succeeded) { return Forbid(); }

            await _meetupRepository.UpdateMeetupAsync(name, newModel);

            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            if (!await _meetupRepository.MeetupAlreadyExistsAsync(name))
            {
                return NotFound(name);
            }

            var meetup = await _meetupRepository.GetMeetupAsync(name);

            var authorizationResult = _authorizationService.AuthorizeAsync(User, meetup, new ResourceOperationRequirement(OperationType.Delete)).Result;

            if (!authorizationResult.Succeeded) { return Forbid();}

            await _meetupRepository.DeleteMeetupAsync(meetup);

            return Ok();
        }
    }
}

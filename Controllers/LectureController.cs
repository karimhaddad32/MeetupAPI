using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Controllers
{
    [Route("api/meetup/{meetupName}/lecture")]
    public class LectureController(IMeetupRepository meetupRepository, IMapper mapper, ILogger<LectureController> logger) : ControllerBase
    {
        IMeetupRepository _meetupRepository = meetupRepository;
        IMapper _mapper = mapper;
        ILogger<LectureController> _logger = logger;

        [HttpPost]
        public async Task<ActionResult> Post(string meetupName, [FromBody] LectureDto lectureDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _meetupRepository.MeetupAlreadyExistsAsync(meetupName))
            {
                return NotFound();
            }
            var lectureModel = _mapper.Map<Lecture>(lectureDto);

            await _meetupRepository.AddLectureAsync(meetupName, lectureModel);

            return Created($"api/meetup/{meetupName}", null);
        }

        [HttpGet]
        public async Task<ActionResult> Get(string meetupName)
        {
            if (!await _meetupRepository.MeetupAlreadyExistsAsync(meetupName))
            {
                return NotFound();
            }

            var lectures = await _meetupRepository.GetLecturesAsync(meetupName);

            var lecturesDtos = _mapper.Map<List<LectureDto>>(lectures);

            return Ok(lecturesDtos);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string meetupName)
        {
            if (!await _meetupRepository.MeetupAlreadyExistsAsync(meetupName))
            {
                return NotFound();
            }

            _logger.LogWarning($"Lectures for meetup {meetupName} were deleted!");

            await _meetupRepository.DeleteLecturesAsync(meetupName);

            return NoContent();
        }

        [HttpDelete("{lectureId}")]
        public async Task<ActionResult> Delete(string meetupName, int lectureId)
        {
            if (!await _meetupRepository.MeetupAlreadyExistsAsync(meetupName))
            {
                return NotFound();
            }

            await _meetupRepository.DeleteLectureAsync(meetupName, lectureId);

            return NoContent();
        }
    }
}

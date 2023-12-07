using MeetupAPI.Entities;
using MeetupAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Controllers
{
    [Route("api/meetup")]
    public class MeetupController(IMeetupRepository meetupRepository) : Controller
    {
        private readonly IMeetupRepository _meetupRepository = meetupRepository;

        [HttpGet]
        public async Task<ActionResult<List<Meetup>>> GetMeetupsAsync()
        {
            return Ok(await _meetupRepository.GetAllMeetupsAsync());
        }
    }
}

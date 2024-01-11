using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.Controllers
{
    [Route("config")]
    public class ConfigControllercs : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigControllercs(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpOptions("reload")]
        public ActionResult Reload()
        {
            try
            {
                ((IConfigurationRoot)_configuration).Reload();

                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}

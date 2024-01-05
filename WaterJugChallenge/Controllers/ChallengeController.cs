using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WaterJugChallenge.BLL.Interfaces;
using WaterJugChallenge.Core.Dtos;

namespace WaterJugChallenge.Controllers
{
    [Route("api/challenges")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IWaterJugChallenge _waterJugChallenge;
        public ChallengeController(IWaterJugChallenge waterJugChallenge)
        {
            _waterJugChallenge = waterJugChallenge;
        }

        [HttpPost]
        [SwaggerOperation("Water Jug Challenge")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<WaterJugChallengeResponseDto>), ContentTypes = new string[] { "application/json" })]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult WaterJugChallenge([FromBody] WaterJugChallengeCreateDto waterJugChallenge)
        {
            try
            {
                return StatusCode(200, _waterJugChallenge.MeasureWater(waterJugChallenge));
            }
            catch (Exception)
            {
                return StatusCode(500, "No Solution.");
            }
        }
    }
}

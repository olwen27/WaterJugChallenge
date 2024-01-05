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

        /// <summary>
        /// Endpoint for solving the Water Jug Challenge.
        /// </summary>
        /// <param name="waterJugChallenge">Input data for the Water Jug Challenge.</param>
        /// <returns>
        ///   Returns a list of WaterJugChallengeResponseDto representing the solution if successful (Status 200 OK).
        ///   If no solution is found, returns a 500 Internal Server Error with an error message.
        /// </returns>
        [HttpPost]
        [SwaggerOperation("Water Jug")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<WaterJugChallengeResponseDto>), ContentTypes = new string[] { "application/json" })]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult WaterJug([FromForm] WaterJugChallengeCreateDto waterJugChallenge)
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

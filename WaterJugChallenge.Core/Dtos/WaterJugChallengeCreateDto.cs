using System.ComponentModel.DataAnnotations;
using WaterJugChallenge.Core.Dtos.Common;

namespace WaterJugChallenge.Core.Dtos
{
    /// <summary>
    /// Water Jug Challenge Create Dto
    /// </summary>
    public class WaterJugChallengeCreateDto : WaterJugChallengeDto
    {
        [Required(ErrorMessage = "TargetAmount is required")]
        public int TargetAmount  { get; set; }
    }
}

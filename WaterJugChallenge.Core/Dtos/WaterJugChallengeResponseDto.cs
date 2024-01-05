using WaterJugChallenge.Core.Dtos.Common;

namespace WaterJugChallenge.Core.Dtos
{
    /// <summary>
    /// Response of the jug water
    /// </summary>
    public class WaterJugChallengeResponseDto : WaterJugChallengeDto
    {
        /// <summary>
        /// Represent the Actions
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}

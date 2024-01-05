using WaterJugChallenge.Core.Dtos.Common;

namespace WaterJugChallenge.Core.Dtos
{
    public class WaterJugChallengeResponseDto : WaterJugChallengeDto
    {
        public string Message { get; set; } = string.Empty;
    }
}

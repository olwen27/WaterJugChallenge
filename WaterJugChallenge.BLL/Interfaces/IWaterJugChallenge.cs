using WaterJugChallenge.Core.Dtos;

namespace WaterJugChallenge.BLL.Interfaces
{
    public interface IWaterJugChallenge
    {
        List<WaterJugChallengeResponseDto> MeasureWater(WaterJugChallengeCreateDto waterJugChallengeCreateDto);
    }
}

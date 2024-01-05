using WaterJugChallenge.Core.Dtos;

namespace WaterJugChallenge.BLL.Interfaces
{
    public interface IWaterJugChallenge
    {
        /// <summary>
        /// Measures water.
        /// </summary>
        /// <param name="waterJugChallengeCreateDto">Input data containing bucket capacities and target amount.</param>
        /// <returns>
        /// List of WaterJugChallengeResponseDto representing the steps taken to achieve the target amount.
        /// The method determines the appropriate bucket to start the simulation based on the closest value to the target amount.
        /// Throws an ArgumentException if the input parameters are invalid.
        /// </returns>
        List<WaterJugChallengeResponseDto> MeasureWater(WaterJugChallengeCreateDto waterJugChallengeCreateDto);
    }
}

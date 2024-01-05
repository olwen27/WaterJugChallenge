using WaterJugChallenge.BLL.Services;
using WaterJugChallenge.Core.Dtos;
using Xunit;

namespace WaterJugChallenge.BLL.UnitTest.Tests
{
    public class WaterJugChallengeServiceTests
    {

        private readonly WaterJugChallengeService _waterJugChallengeService;

        public WaterJugChallengeServiceTests()
        {
            _waterJugChallengeService = new WaterJugChallengeService();
        }

        [Fact]
        public void CanMeasureWater_InvalidInput_ThrowsArgumentException()
        {
            // Arrange
            var dto = new WaterJugChallengeCreateDto { BucketX = 0, BucketY = 5, TargetAmount = 4 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _waterJugChallengeService.MeasureWater(dto));
        }

        [Fact]
        public void CanMeasureWater_InvalidInput_TargetHigherThanBuckets_ThrowsArgumentException()
        {
            // Arrange
            var dto = new WaterJugChallengeCreateDto { BucketX = 33, BucketY = 45, TargetAmount = 55 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _waterJugChallengeService.MeasureWater(dto));
        }

        [Fact]
        public void CanMeasureWater_InvalidInput_EvenAndOddValues_ThrowsArgumentException()
        {
            // Arrange
            var dto = new WaterJugChallengeCreateDto { BucketX = 2, BucketY = 6, TargetAmount = 5 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _waterJugChallengeService.MeasureWater(dto));
        }

        [Fact]
        public void BucketProblem_ValidInput_ReturnsResultList()
        {
            // Arrange
            var dto = new WaterJugChallengeCreateDto { BucketX = 2, BucketY = 10, TargetAmount = 4 };

            // Act
            var result = _waterJugChallengeService.BucketProblemBucketX(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void BucketProblem2_ValidInput_ReturnsResultList()
        {
            // Arrange
            var dto = new WaterJugChallengeCreateDto { BucketX = 2, BucketY = 100, TargetAmount = 96 };

            // Act
            var result = _waterJugChallengeService.BucketProblemBucketY(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void FindNearestValue_ValidInput_ReturnsNearestValue()
        {
            // Arrange
            var dto = new WaterJugChallengeCreateDto { BucketX = 2, BucketY = 10, TargetAmount = 4 };

            // Act
            var result = _waterJugChallengeService.FindNearestValue(dto);

            // Assert
            Assert.Equal(2, result);
        }
    }
}

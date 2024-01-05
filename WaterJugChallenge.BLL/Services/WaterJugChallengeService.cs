using WaterJugChallenge.BLL.Interfaces;
using WaterJugChallenge.Core.Dtos;

namespace WaterJugChallenge.BLL.Services
{
    public class WaterJugChallengeService : IWaterJugChallenge
    {
        public List<WaterJugChallengeResponseDto> MeasureWater(WaterJugChallengeCreateDto waterJugChallengeCreateDto)
        {

            if (CanMeasureWater(waterJugChallengeCreateDto))
            {
                if (FindNearestValue(waterJugChallengeCreateDto) == waterJugChallengeCreateDto.BucketX)
                {
                    return BucketProblemBucketX(waterJugChallengeCreateDto);
                }

                if (FindNearestValue(waterJugChallengeCreateDto) == waterJugChallengeCreateDto.BucketY)
                {
                    return BucketProblemBucketY(waterJugChallengeCreateDto);
                }
            }

            throw new ArgumentException("Invalid jug capacities or target amount.");
        }

        public bool CanMeasureWater(WaterJugChallengeCreateDto waterJugChallengeCreateDto)
        {
            if (waterJugChallengeCreateDto.BucketX <= 0 || waterJugChallengeCreateDto.BucketY <= 0 || waterJugChallengeCreateDto.TargetAmount < 0)
            {
                return false;
            }

            if (waterJugChallengeCreateDto.BucketX < waterJugChallengeCreateDto.TargetAmount && waterJugChallengeCreateDto.BucketY < waterJugChallengeCreateDto.TargetAmount)
            {
                return false;
            }

            // Check if all values ​​are even or odd
            if (IsOdd(waterJugChallengeCreateDto.BucketX) && IsOdd(waterJugChallengeCreateDto.BucketY) && IsOdd(waterJugChallengeCreateDto.TargetAmount))
            {
                return true;
            }
            else if (!IsOdd(waterJugChallengeCreateDto.BucketX) && !IsOdd(waterJugChallengeCreateDto.BucketY) && !IsOdd(waterJugChallengeCreateDto.TargetAmount))
            {
                return true;
            }
        

            return false;
        }

        public List<WaterJugChallengeResponseDto> BucketProblemBucketX(WaterJugChallengeCreateDto waterJugChallengeCreateDto)
        {
            // Initialize variables
            int currentX = 0;
            int currentY = 0;
            bool start = true;
            var waterJugChallenge = new List<WaterJugChallengeResponseDto>();

            // Loop until either bucket X or Y contains the desired amount
            while (currentX != waterJugChallengeCreateDto.TargetAmount && currentY != waterJugChallengeCreateDto.TargetAmount)
            {
                // Fill bucket X
                currentX = waterJugChallengeCreateDto.BucketX;

                // Initial filling of bucket X
                if (start)
                {
                    waterJugChallenge.Add(new WaterJugChallengeResponseDto
                    {
                        BucketX = currentX,
                        BucketY = currentY,
                        Message = "Fill bucket X"
                    });
                }
                start = false;

                // Check if the goal is already reached
                if (currentX == waterJugChallengeCreateDto.TargetAmount || currentY == waterJugChallengeCreateDto.TargetAmount)
                    return waterJugChallenge;

                // Transfer from bucket X to bucket Y
                int transferAmount = Math.Min(currentX, waterJugChallengeCreateDto.BucketY - currentY);

                // Update the current values in buckets
                currentX -= transferAmount;
                currentY += transferAmount;

                // Output the transfer from bucket X to bucket Y
                waterJugChallenge.Add(new WaterJugChallengeResponseDto
                {
                    BucketX = currentX,
                    BucketY = currentY,
                    Message = "Transfer from bucket X to bucket Y"
                });

                // Check if the goal is reached after the transfer
                if (currentX == waterJugChallengeCreateDto.TargetAmount || currentY == waterJugChallengeCreateDto.TargetAmount)
                    return waterJugChallenge;


                // If bucket X is empty, fill it again
                if (currentX == 0)
                {
                    currentX = waterJugChallengeCreateDto.BucketX;

                    waterJugChallenge.Add(new WaterJugChallengeResponseDto
                    {
                        BucketX = currentX,
                        BucketY = currentY,
                        Message = "Fill bucket X"
                    });
                }

                // Check if the goal is reached after the transfer or empty of X
                if (currentX == waterJugChallengeCreateDto.TargetAmount || currentY == waterJugChallengeCreateDto.TargetAmount)
                    return waterJugChallenge;

            }

            // Goal not reached
            return waterJugChallenge;
        }

        public List<WaterJugChallengeResponseDto> BucketProblemBucketY(WaterJugChallengeCreateDto waterJugChallengeCreateDto)
        {
            // Initialize variables
            int currentX = 0;
            int currentY = 0;
            bool start = true;
            var waterJugChallenge = new List<WaterJugChallengeResponseDto>();


            // Loop until either bucket X or Y contains the desired amount
            while (currentX != waterJugChallengeCreateDto.TargetAmount && currentY != waterJugChallengeCreateDto.TargetAmount)
            {
                // Initial filling of bucket Y
                if (start)
                {
                    currentY = waterJugChallengeCreateDto.BucketY;

                    waterJugChallenge.Add(new WaterJugChallengeResponseDto
                    {
                        BucketX = currentX,
                        BucketY = currentY,
                        Message = "Fill bucket Y"
                    });
                }

                start = false;

                // Check if the goal is already reached
                if (currentX == waterJugChallengeCreateDto.TargetAmount || currentY == waterJugChallengeCreateDto.TargetAmount)
                    return waterJugChallenge;


                // Transfer from bucket X to bucket Y
                int transferAmount = Math.Min(currentY, waterJugChallengeCreateDto.BucketX - currentX);

                if (transferAmount == 0)
                {
                    // Empty bucket X
                    transferAmount = waterJugChallengeCreateDto.BucketX;
                    currentX -= transferAmount;
                }
                else
                {
                    // Fill bucket Y
                    currentX += transferAmount;
                }

                // Output the current state after the transfer
                if (currentX == 0)
                {
                    waterJugChallenge.Add(new WaterJugChallengeResponseDto
                    {
                        BucketX = currentX,
                        BucketY = currentY,
                        Message = "Empty bucket X"
                    });
                }

                // Update the amount in bucket Y
                currentY -= transferAmount;

                // Output the transfer from bucket Y to bucket X
                if (currentY != waterJugChallengeCreateDto.TargetAmount)
                {
                    waterJugChallenge.Add(new WaterJugChallengeResponseDto
                    {
                        BucketX = currentX,
                        BucketY = currentY,
                        Message = "Transfer from bucket Y to bucket X"
                    });
                }
                else
                {
                    waterJugChallenge.Add(new WaterJugChallengeResponseDto
                    {
                        BucketX = waterJugChallengeCreateDto.BucketX,
                        BucketY = currentY,
                        Message = "Transfer from bucket Y to bucket X"
                    });
                }

                // Check if the goal is reached after the transfer
                if (currentX == waterJugChallengeCreateDto.TargetAmount || currentY == waterJugChallengeCreateDto.TargetAmount)
                    return waterJugChallenge;

            }

            // Goal not reached
            return waterJugChallenge;
        }



        public int FindNearestValue(WaterJugChallengeCreateDto waterJugChallengeCreateDto)
        {
            // Check if the target is outside the range [x, y]
            if (waterJugChallengeCreateDto.TargetAmount <= waterJugChallengeCreateDto.BucketX)
            {
                return waterJugChallengeCreateDto.BucketX;
            }
            else if (waterJugChallengeCreateDto.TargetAmount >= waterJugChallengeCreateDto.BucketY)
            {
                return waterJugChallengeCreateDto.BucketY;
            }

            // Compare the distance from target to x and y
            int distanceToX = Math.Abs(waterJugChallengeCreateDto.TargetAmount - waterJugChallengeCreateDto.BucketX);
            int distanceToY = Math.Abs(waterJugChallengeCreateDto.TargetAmount - waterJugChallengeCreateDto.BucketY);

            // Return the nearest value
            return distanceToX < distanceToY ? waterJugChallengeCreateDto.BucketX : waterJugChallengeCreateDto.BucketY;
        }

        static bool IsOdd(int number)
        {
            return number % 2 == 0;
        }
    }
}

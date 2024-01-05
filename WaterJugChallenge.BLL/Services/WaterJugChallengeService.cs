using WaterJugChallenge.BLL.Interfaces;
using WaterJugChallenge.Core.Dtos;

namespace WaterJugChallenge.BLL.Services
{
    public class WaterJugChallengeService : IWaterJugChallenge
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

        /// <summary>
        /// Solve the Water Jug Challenge when BucketX is the closest value to the Target Amount.
        /// </summary>
        /// <param name="waterJugChallengeCreateDto">Input data containing bucket capacities and target amount.</param>
        /// <returns>
        /// List of WaterJugChallengeResponseDto representing the steps taken to achieve the target amount.
        /// The simulation continues until the target amount is reached in either bucket or no further progress is possible.
        /// </returns>
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

        /// <summary>
        /// Solve the Water Jug Challenge when BucketY is the closest value to the Target Amount.
        /// </summary>
        /// <param name="waterJugChallengeCreateDto">Input data containing bucket capacities and target amount.</param>
        /// <returns>
        /// List of WaterJugChallengeResponseDto representing the steps taken to achieve the target amount.
        /// The simulation continues until the target amount is reached in either bucket or no further progress is possible.
        /// </returns>
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

        /// <summary>
        /// Validate if water can measure 
        /// </summary>
        /// <param name="waterJugChallengeCreateDto">Input data containing bucket capacities and target amount.</param>
        /// <returns>True/false if water can measure</returns>
        public bool CanMeasureWater(WaterJugChallengeCreateDto waterJugChallengeCreateDto)
        {
            //Check values are not equal to 0
            if (waterJugChallengeCreateDto.BucketX <= 0 || waterJugChallengeCreateDto.BucketY <= 0 || waterJugChallengeCreateDto.TargetAmount <= 0)
            {
                return false;
            }

            //Check if target is higher than BucketX and BucketY
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

        /// <summary>
        /// Find the closest bucket to the TargetAmount
        /// </summary>
        /// <param name="waterJugChallengeCreateDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Validate if number is odd or even
        /// </summary>
        /// <param name="number"></param>
        /// <returns>true/false if number is odd or even</returns>
        public bool IsOdd(int number)
        {
            return number % 2 == 0;
        }
    }
}

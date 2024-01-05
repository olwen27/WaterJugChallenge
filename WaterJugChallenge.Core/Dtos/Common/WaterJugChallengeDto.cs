using System.ComponentModel.DataAnnotations;

namespace WaterJugChallenge.Core.Dtos.Common
{
    /// <summary>
    /// This class contains the common values for Dtos
    /// </summary>
    public class WaterJugChallengeDto
    {
        [Required(ErrorMessage = "BucketX is required")]
        public int BucketX { get; set; }
        
        [Required(ErrorMessage = "BucketY is required")]
        public int BucketY { get; set; }
    }
}

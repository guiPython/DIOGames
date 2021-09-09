using System.ComponentModel.DataAnnotations;

namespace DIOGames.InputModel
{
    public class InputModelGame
    {
        [Required()]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "Invalid game name length, must be in range (3,30)")]
        public string Name { get; set; }

        [Required()]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid producer name length, must be in range (3,30)")]
        public string Producer { get; set; }

        [Required()]
        [Range(1,1000,ErrorMessage = "Invalid game price, must be in range (1,1000)")]
        public decimal Price { get; set; }
    }
}

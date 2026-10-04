using SuperSnakeGameAPI.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperSnakeGameAPI.Core.DTO
{
    /// <summary>
    /// Acts as a Data Transfer Object (DTO) for adding a new player.
    /// </summary>
    public class PlayerAddRequest
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Range(0, int.MaxValue)]
        public int? Highscore { get; set; }

        /// <summary>
        /// Converts the PlayerAddRequest DTO to a Player entity.
        /// </summary>
        /// <returns>A Player entity with the properties set from the DTO.</returns>
        public Player ToPlayer()
        {
            return new Player
            {
                Name = this.Name,
                Email = this.Email,
                Highscore = this.Highscore
            };
        }
    }
}

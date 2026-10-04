using SuperSnakeGameAPI.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperSnakeGameAPI.Core.DTO
{
    /// <summary>
    /// Represents a request to update an existing player's information.
    /// </summary>
    public class PlayerUpdateRequest
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Range(0, int.MaxValue)]
        public int? Highscore { get; set; }

        /// <summary>
        /// Converts the PlayerUpdateRequest DTO to a Player entity.
        /// </summary>
        /// <returns>A Player entity with the updated information.</returns>
        public Player ToPlayer()
        {
            return new Player
            {
                ID = this.ID,
                Name = this.Name,
                Email = this.Email,
                Highscore = this.Highscore
            };
        }
    }
}

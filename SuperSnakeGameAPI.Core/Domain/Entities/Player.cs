using System.ComponentModel.DataAnnotations;

namespace SuperSnakeGameAPI.Core.Domain.Entities
{
    /// <summary>
    /// Player entity representing a player in the Star Race game.
    /// </summary>
    public class Player
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(40)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Highscore { get; set; } = 0;
    }
}

using System.ComponentModel.DataAnnotations;

namespace SuperSnakeGameAPI.Core.Domain.Entities
{
    /// <summary>
    /// Player entity representing a player in the Star Race game.
    /// </summary>
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        [Range(0, int.MaxValue)]
        public int Highscore { get; set; }

        public override string ToString()
        {
            return $"Player Id: {Id}, Highscore: {Highscore}";
        }
    }
}

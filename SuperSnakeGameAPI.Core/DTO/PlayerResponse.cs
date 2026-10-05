using SuperSnakeGameAPI.Core.Domain.Entities;

namespace SuperSnakeGameAPI.Core.DTO
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for player responses, encapsulating player information such as Id and Highscore.
    /// </summary>
    public class PlayerResponse
    {
        public Guid Id { get; set; }
        public int Highscore { get; set; }

        /// <summary>
        /// Compares the current PlayerResponse instance with another object for equality based on the values of Id and Highscore.
        /// </summary>
        /// <param name="obj">The object to compare with the current PlayerResponse instance.</param>
        /// <returns>True if the specified object is equal to the current PlayerResponse instance; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return obj is PlayerResponse other
                && Id == other.Id
                && Highscore == other.Highscore;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"PlayerResponse: Id={Id}, Highscore={Highscore}";
        }
    }

    public static class PlayerResponseExtensions
    {
        /// <summary>
        /// An extension method to convert a Player entity to a PlayerResponse DTO.
        /// </summary>
        /// <param name="player">The Player entity to convert.</param>
        /// <returns>A PlayerResponse DTO with properties copied from the Player entity.</returns>
        public static PlayerResponse ToPlayerResponse(this Player player)
        {
            return new PlayerResponse
            {
                Id = player.Id,
                Highscore = player.Highscore
            };
        }
    }
}

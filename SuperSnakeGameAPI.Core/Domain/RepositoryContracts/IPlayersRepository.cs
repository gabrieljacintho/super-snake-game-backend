using SuperSnakeGameAPI.Core.Domain.Entities;

namespace SuperSnakeGameAPI.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents a repository contract for managing player entities in the application.
    /// </summary>
    public interface IPlayersRepository
    {
        /// <summary>
        /// Gets a list of all players asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all players.</returns>
        Task<List<Player>> GetAllPlayersAsync();

        /// <summary>
        /// Gets a player by their email asynchronously.
        /// </summary>
        /// <param name="email">The email of the player to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the player with the specified email, or null if not found.</returns>
        Task<Player?> GetPlayerByEmailAsync(string email);

        /// <summary>
        /// Adds a new player asynchronously.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added player.</returns>
        Task<Player> AddPlayerAsync(Player player);

        /// <summary>
        /// Updates an existing player asynchronously.
        /// </summary>
        /// <param name="player">The player entity containing the updated information to be saved. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated player entity.</returns>
        Task<Player> UpdatePlayerAsync(Player player);

        /// <summary>
        /// Deletes a player by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the player to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the player
        /// was successfully deleted; otherwise, <see langword="false"/>.</returns>
        Task<bool> DeletePlayerByIDAsync(Guid id);
    }
}

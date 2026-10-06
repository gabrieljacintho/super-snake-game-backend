using SuperSnakeGameAPI.Core.DTOs;

namespace SuperSnakeGameAPI.Core.ServiceContracts
{
    /// <summary>
    /// Represents a service contract for managing player-related operations in the application.
    /// </summary>
    public interface IPlayersService
    {
        /// <summary>
        /// Gets or creates a player with id asynchronously.
        /// </summary>
        /// <param name="id">The Id of the player to get or create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated PlayerResponse object.</returns>
        Task<PlayerResponse> GetOrCreatePlayerAsync(Guid id);

        /// <summary>
        /// Updates the highscore of a player asynchronously.
        /// </summary>
        /// <param name="id">The Id of the player whose highscore is to be updated.</param>
        /// <param name="newHighscore">The new highscore of the player.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated PlayerResponse object.</returns>
        Task<PlayerResponse> UpdateHighscoreAsync(Guid id, int newHighscore);

        /// <summary>
        /// Deletes a player by their Id asynchronously.
        /// </summary>
        /// <param name="id">The Id of the player to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the player was deleted; otherwise, false.</returns>
        Task<bool> DeletePlayerByIdAsync(Guid id);

        /// <summary>
        /// Gets a list of all players asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of PlayerResponse objects.</returns>
        Task<List<PlayerResponse>> GetAllPlayersAsync();
    }
}

using SuperSnakeGameAPI.Core.DTO;

namespace SuperSnakeGameAPI.Core.ServiceContracts
{
    /// <summary>
    /// Represents a service contract for managing player-related operations in the application.
    /// </summary>
    public interface IPlayersService
    {
        /// <summary>
        /// Adds a new player asynchronously.
        /// </summary>
        /// <param name="playerAddRequest">The player request object containing the details of the player to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added PlayerResponse object.</returns>
        Task<PlayerResponse> AddPlayerAsync(PlayerAddRequest? playerAddRequest);
        
        /// <summary>
        /// Updates an existing player asynchronously.
        /// </summary>
        /// <param name="playerUpdateRequest">The player request object containing the updated details of the player.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated PlayerResponse object.</returns>
        Task<PlayerResponse> UpdatePlayerAsync(PlayerUpdateRequest? playerUpdateRequest);
        
        /// <summary>
        /// Deletes a player by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the player to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the player was deleted; otherwise, false.</returns>
        Task<bool> DeletePlayerByIDAsync(Guid? id);

        /// <summary>
        /// Gets a list of all players asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of PlayerResponse objects.</returns>
        Task<List<PlayerResponse>> GetAllPlayersAsync();

        /// <summary>
        /// Gets a player by their email asynchronously.
        /// </summary>
        /// <param name="email">The email of the player to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the PlayerResponse object if found; otherwise, null.</returns>
        Task<PlayerResponse?> GetPlayerByEmailAsync(string email);
    }
}

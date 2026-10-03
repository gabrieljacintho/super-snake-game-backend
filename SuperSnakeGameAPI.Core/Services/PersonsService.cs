using SuperSnakeGameAPI.Core.DTO;
using SuperSnakeGameAPI.Core.ServiceContracts;

namespace SuperSnakeGameAPI.Core.Services
{
    public class PlayersService : IPlayersService
    {
        public Task<PlayerResponse> AddPlayerAsync(PlayerAddRequest playerRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePlayerByIDAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerResponse>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerResponse?> GetPlayerByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerResponse> UpdatePlayerAsync(PlayerUpdateRequest playerRequest)
        {
            throw new NotImplementedException();
        }
    }
}

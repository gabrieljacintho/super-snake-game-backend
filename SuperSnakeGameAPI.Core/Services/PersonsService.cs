using SuperSnakeGameAPI.Core.Domain.RepositoryContracts;
using SuperSnakeGameAPI.Core.DTO;
using SuperSnakeGameAPI.Core.ServiceContracts;

namespace SuperSnakeGameAPI.Core.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository _playersRepository;

        public PlayersService(IPlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;
        }

        public Task<PlayerResponse> AddPlayerAsync(PlayerAddRequest? playerAddRequest)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerResponse> UpdatePlayerAsync(PlayerUpdateRequest? playerUpdateRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePlayerByIDAsync(Guid? id)
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
    }
}

using SuperSnakeGameAPI.Core.Domain.Entities;
using SuperSnakeGameAPI.Core.Domain.RepositoryContracts;
using SuperSnakeGameAPI.Core.DTOs;
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

        public async Task<PlayerResponse> GetOrCreatePlayerAsync(Guid id)
        {
            Player player = await GetOrCreatePlayerEntityAsync(id);

            return player.ToPlayerResponse();
        }

        private async Task<Player> GetOrCreatePlayerEntityAsync(Guid id)
        {
            Player? player = await _playersRepository.GetPlayerByIdAsync(id);

            if (player == null)
            {
                player = new Player()
                {
                    Id = id
                };

                await _playersRepository.AddPlayerAsync(player);
            }

            return player;
        }

        public async Task<PlayerResponse> UpdateHighscoreAsync(Guid id, int newHighscore)
        {
            Player? matchingPlayer = await GetOrCreatePlayerEntityAsync(id);

            if (newHighscore <= matchingPlayer.Highscore)
            {
                return matchingPlayer.ToPlayerResponse();
            }

            matchingPlayer.Highscore = newHighscore;

            await _playersRepository.UpdatePlayerAsync(matchingPlayer);

            return matchingPlayer.ToPlayerResponse();
        }

        public async Task<bool> DeletePlayerByIdAsync(Guid id)
        {
            return await _playersRepository.DeletePlayerByIdAsync(id);
        }

        public async Task<List<PlayerResponse>> GetAllPlayersAsync()
        {
            var players = await _playersRepository.GetAllPlayersAsync();

            return players.Select(player => player.ToPlayerResponse()).ToList();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarRaceAPI.Core.ServiceContracts;
using StarRaceAPI.Infrastructure;
using StarRaceAPI.Models;

namespace StarRace.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PlayerRepository> _logger;

        public PlayerRepository(ApplicationDbContext dbContext, ILogger<PlayerRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Player> AddOrUpdatePlayerAsync(Player player)
        {
            Player? existingPlayer = await _dbContext.Players.FirstOrDefaultAsync(p => p.Email == player.Email);

            if (existingPlayer == null)
            {
                _dbContext.Players.Add(player);
            }
            else
            {
                existingPlayer.UserName = player.UserName;
                existingPlayer.Highscore = player.Highscore;
            }

            await _dbContext.SaveChangesAsync();

            if (existingPlayer == null)
            {
                _logger.LogInformation("New player with email {Email} has been added.", player.Email);
            }
            else
            {
                _logger.LogInformation("Player with email {Email} has been updated.", player.Email);
            }

            return player;
        }

        public async Task<bool> DeletePlayerAsync(string email)
        {
            Player? player = await _dbContext.Players.FirstOrDefaultAsync(p => p.Email == email);

            if (player != null)
            {
                _dbContext.Players.Remove(player);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Player with email {Email} has been deleted.", email);

                return true;
            }

            _logger.LogWarning("Player with email {Email} not found. Deletion failed.", email);

            return false;
        }

        public async Task<Player[]> GetAllPlayersAsync()
        {
            Player[] players = await _dbContext.Players.ToArrayAsync();

            _logger.LogInformation("Retrieved all players. Count: {Count}", players.Length);

            return players;
        }

        public async Task<Player?> GetPlayerByEmailAsync(string email)
        {
            Player? player = await _dbContext.Players.FirstOrDefaultAsync(p => p.Email == email);

            if (player != null)
            {
                _logger.LogInformation("Player with email {Email} retrieved successfully.", email);
            }
            else
            {
                _logger.LogWarning("Player with email {Email} not found.", email);
            }

            return player;
        }
    }
}

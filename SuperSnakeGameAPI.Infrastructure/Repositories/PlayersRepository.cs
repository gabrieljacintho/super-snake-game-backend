using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperSnakeGameAPI.Core.Domain.Entities;
using SuperSnakeGameAPI.Core.Domain.RepositoryContracts;
using SuperSnakeGameAPI.Infrastructure.DbContext;

namespace SuperSnakeGameAPI.Infrastructure.Repositories
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayersRepository(ApplicationDbContext dbContext, ILogger<PlayersRepository> logger)
        {
            _dbContext = dbContext;
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            _dbContext.Players.Add(player);

            await _dbContext.SaveChangesAsync();

            return player;
        }

        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            Player? existingPlayer = await _dbContext.Players.FirstOrDefaultAsync(p => p.Email == player.Email);

            if (existingPlayer == null)
            {
                return player;
            }

            existingPlayer.UserName = player.UserName;
            existingPlayer.Highscore = player.Highscore;

            await _dbContext.SaveChangesAsync();

            return existingPlayer;
        }

        public async Task<bool> DeletePlayerByIDAsync(Guid id)
        {
            _dbContext.Players.RemoveRange(_dbContext.Players.Where(p => p.Email == email));

            int changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            return await _dbContext.Players.ToListAsync();
        }

        public async Task<Player?> GetPlayerByEmailAsync(string email)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}

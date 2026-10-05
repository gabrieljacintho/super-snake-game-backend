using Microsoft.EntityFrameworkCore;
using SuperSnakeGameAPI.Core.Domain.Entities;
using SuperSnakeGameAPI.Core.Domain.RepositoryContracts;
using SuperSnakeGameAPI.Infrastructure.DbContext;

namespace SuperSnakeGameAPI.Infrastructure.Repositories
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayersRepository(ApplicationDbContext dbContext)
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
            Player? existingPlayer = await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == player.Id);

            if (existingPlayer == null)
            {
                return player;
            }

            existingPlayer.Highscore = player.Highscore;

            await _dbContext.SaveChangesAsync();

            return existingPlayer;
        }

        public async Task<bool> DeletePlayerByIdAsync(Guid id)
        {
            _dbContext.Players.RemoveRange(_dbContext.Players.Where(p => p.Id == id));

            int changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            return await _dbContext.Players.ToListAsync();
        }

        public async Task<Player?> GetPlayerByIdAsync(Guid id)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

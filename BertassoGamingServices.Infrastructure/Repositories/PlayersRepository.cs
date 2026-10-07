using Microsoft.EntityFrameworkCore;
using BertassoGamingServices.Core.Domain.Entities;
using BertassoGamingServices.Core.Domain.RepositoryContracts;
using BertassoGamingServices.Infrastructure.DbContext;

namespace BertassoGamingServices.Infrastructure.Repositories
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
            Player? existingPlayer = await _dbContext.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == player.Id);

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
            return await _dbContext.Players.Include(p => p.User).ToListAsync();
        }

        public async Task<Player?> GetPlayerByIdAsync(Guid id)
        {
            return await _dbContext.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

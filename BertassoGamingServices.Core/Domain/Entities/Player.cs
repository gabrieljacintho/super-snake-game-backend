using BertassoGamingServices.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BertassoGamingServices.Core.Domain.Entities
{
    /// <summary>
    /// Player entity representing a player in the game.
    /// </summary>
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        public ApplicationUser? User { get; set; }

        [NotMapped]
        public string? Name => User?.Name;

        [Range(0, int.MaxValue)]
        public int Highscore { get; set; }

        public override string ToString()
        {
            return $"Player Id: {Id}, Highscore: {Highscore}";
        }
    }
}

using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BertassoGamingServices.Core.ServiceContracts;
using BertassoGamingServices.Web.Extensions;

namespace BertassoGamingServices.Web.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        [HttpGet("highscore")]
        public async Task<IActionResult> GetHighscore()
        {
            if (!this.TryGetUserId(out Guid userId))
            {
                return Unauthorized();
            }

            return Ok(await _playersService.GetOrCreatePlayerAsync(userId));
        }

        [HttpPut("highscore")]
        public async Task<IActionResult> UpdateHighscore([FromBody] int newHighscore)
        {
            if (!this.TryGetUserId(out Guid userId))
            {
                return Unauthorized();
            }

            return Ok(await _playersService.UpdateHighscoreAsync(userId, newHighscore));
        }
    }
}

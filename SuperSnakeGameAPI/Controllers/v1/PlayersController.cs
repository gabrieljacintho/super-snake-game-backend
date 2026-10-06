using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSnakeGameAPI.Core.ServiceContracts;
using System.Security.Claims;

namespace SuperSnakeGameAPI.Web.Controllers.v1
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
            if (!TryGetUserId(out Guid userId))
            {
                return Unauthorized();
            }

            return Ok(await _playersService.GetOrCreatePlayerAsync(userId));
        }

        [HttpPut("highscore")]
        public async Task<IActionResult> UpdateHighscore([FromBody] int newHighscore)
        {
            if (!TryGetUserId(out Guid userId))
            {
                return Unauthorized();
            }

            return Ok(await _playersService.UpdateHighscoreAsync(userId, newHighscore));
        }

        private bool TryGetUserId(out Guid userId)
        {
            string? userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out userId))
            {
                userId = Guid.Empty;
                return false;
            }

            return true;
        }
    }
}

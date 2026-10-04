using Microsoft.AspNetCore.Mvc;
using SuperSnakeGameAPI.Core.DTO;
using SuperSnakeGameAPI.Infrastructure.DbContext;

namespace SuperSnakeGameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ApplicationDbContext context, ILogger<PlayerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(PlayerAddRequest playerAddRequest)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
        }

        [HttpGet("highscore")]
        public async Task<IActionResult> GetHighscore()
        {
        }

        [HttpPut("highscore")]
        public async Task<IActionResult> UpdateHighscore()
        {
        }
    }
}

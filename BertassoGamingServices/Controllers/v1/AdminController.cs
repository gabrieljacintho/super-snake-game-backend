using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BertassoGamingServices.Core.Domain.IdentityEntities;
using BertassoGamingServices.Core.ServiceContracts;

namespace BertassoGamingServices.Web.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IPlayersService _playersService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IPlayersService playersService, UserManager<ApplicationUser> userManager)
        {
            _playersService = playersService;
            _userManager = userManager;
        }

        [HttpGet("players")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            return Ok(await _playersService.GetAllPlayersAsync());
        }

        [HttpDelete("players/{id:guid}")]
        public async Task<IActionResult> DeletePlayerByIdAsync(Guid id)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(errorMessage);
            }

            return NoContent();
        }
    }
}

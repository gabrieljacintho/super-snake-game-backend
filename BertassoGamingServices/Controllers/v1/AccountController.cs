using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BertassoGamingServices.Core.Domain.IdentityEntities;
using BertassoGamingServices.Core.DTOs;
using BertassoGamingServices.Core.Enums;
using BertassoGamingServices.Core.ServiceContracts;
using System.Security.Claims;

namespace BertassoGamingServices.Web.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IPlayersService _playersService;

        public AccountController(UserManager<ApplicationUser> userManager, IJwtService jwtService, IPlayersService playersService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _playersService = playersService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                Name = registerRequest.Name
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(errorMessage);
            }

            await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());

            await _playersService.GetOrCreatePlayerAsync(user.Id);

            AuthenticationResponse authenticationResponse = await RefreshToken(user);

            return Ok(authenticationResponse);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            AuthenticationResponse authenticationResponse = await RefreshToken(user);

            return Ok(authenticationResponse);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiration = default;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }
        
        [HttpGet("is-email-already-registered")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            return Ok(user != null);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (string.IsNullOrEmpty(tokenModel.Token) || string.IsNullOrEmpty(tokenModel.RefreshToken))
            {
                return BadRequest("Invalid client request.");
            }

            ClaimsPrincipal? principal;

            try
            {
                principal = _jwtService.GetPrincipalFromJwtToken(tokenModel.Token);
            }
            catch
            {
                return Unauthorized("Invalid token.");
            }

            ApplicationUser? user = principal != null ? await _userManager.GetUserAsync(principal) : null;

            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiration <= DateTime.UtcNow)
            {
                return Unauthorized("Invalid refresh token.");
            }

            return Ok(await RefreshToken(user));
        }

        private async Task<AuthenticationResponse> RefreshToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user, roles);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }
    }
}

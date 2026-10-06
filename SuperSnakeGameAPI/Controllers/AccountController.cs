using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperSnakeGameAPI.Core.Domain.IdentityEntities;
using SuperSnakeGameAPI.Core.DTO;
using SuperSnakeGameAPI.Core.Enums;
using SuperSnakeGameAPI.Core.ServiceContracts;
using System.Security.Claims;

namespace SuperSnakeGameAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Name = registerDTO.Name
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(errorMessage);
            }

            await AddToRoleAsync(user, UserTypeOptions.User);

            AuthenticationResponse authenticationResponse = await RefreshToken(user);

            return Ok(authenticationResponse);
        }

        private async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, UserTypeOptions userType)
        {
            string roleName = userType.ToString();

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
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
            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }
    }
}

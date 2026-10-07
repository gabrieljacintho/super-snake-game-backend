using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SuperSnakeGameAPI.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetModelStateErrorMessage(this ControllerBase controller)
        {
            return string.Join(", ", controller.ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
        }

        public static bool TryGetUserId(this ControllerBase controller, out Guid userId)
        {
            string? userIdString = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out userId))
            {
                userId = Guid.Empty;
                return false;
            }

            return true;
        }
    }
}

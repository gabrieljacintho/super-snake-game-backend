using Microsoft.AspNetCore.Mvc;

namespace SuperSnakeGameAPI.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetModelStateErrorMessage(this ControllerBase controller)
        {
            return string.Join(", ", controller.ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
        }
    }
}

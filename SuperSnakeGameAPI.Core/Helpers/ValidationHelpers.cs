using System.ComponentModel.DataAnnotations;

namespace SuperSnakeGameAPI.Core.Helpers
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// Tries to validate the specified object and throws a ValidationException if validation fails.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <returns>True if the object is valid; otherwise, false.</returns>
        public static bool TryValidateObject(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            return isValid;
        }
    }
}

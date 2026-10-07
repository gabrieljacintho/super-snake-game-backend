using System.ComponentModel.DataAnnotations;

namespace BertassoGamingServices.Core.Helpers
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// Tries to validate the specified object and throws a ValidationException if validation fails.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        public static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FormAPI.Infrastructure.Validations
{
    /// <summary>
    /// Validates integer value
    /// </summary>
    /// <param name="errorMessage">Error message</param>
    /// <param name="minValue">Minimum value accepted</param>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class IsValidIntegerAttribute(string errorMessage,int minValue = 0) : ValidationAttribute
    {

        /// <summary>
        /// Validates value
        /// </summary>
        /// <param name="value">Payload value</param>
        /// <param name="validationContext">Handles validation context</param>
        /// <returns>Boolean value to prove it was accepted</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null )
            {
                return new ValidationResult(errorMessage);
            }

            return Convert.ToInt32(value) >= _minValue ? ValidationResult.Success : new ValidationResult(errorMessage);
        }
      

        private readonly int _minValue = minValue;
    }
}

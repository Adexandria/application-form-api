using System.ComponentModel.DataAnnotations;

namespace FormAPI.Infrastructure.Validations
{
    /// <summary>
    /// Validation attribute to allow specific values
    /// </summary>
    /// <param name="values">Values to allow</param>
    /// <param name="errorMessage">Error message</param>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class AllowAttribute(string errorMessage, params string[] values) : ValidationAttribute
    {
        
        /// <summary>
        /// Validates value
        /// </summary>
        /// <param name="value">Payload value</param>
        /// <param name="validationContext">Handles validation context</param>
        /// <returns>Boolean value to prove it was accepted</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(errorMessage);
            }
            var isValid = _values.Contains(value.ToString());

            return isValid ? ValidationResult.Success : new ValidationResult(errorMessage);
        }

        /// <summary>
        /// Values to allow
        /// </summary>
        private readonly string[] _values = values;
    }
}

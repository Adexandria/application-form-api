using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FormAPI.Infrastructure.Validations
{
    /// <summary>
    /// Validates response type for form response
    /// </summary>
    /// <param name="errorMessage"></param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ResponseTypeAttribute(string errorMessage): ValidationAttribute
    {

        /// <summary>
        /// Validates value
        /// </summary>
        /// <param name="value">Payload value</param>
        /// <param name="validationContext">Handles validation context</param>
        /// <returns>Boolean value to prove it was accepted</returns>
        /// 
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not JsonElement node)
            {
                return new ValidationResult(errorMessage);
            };


            return node switch
            {
                JsonElement json when json.ValueKind == JsonValueKind.False || json.ValueKind == JsonValueKind.True => ValidationResult.Success,
                JsonElement json when json.ValueKind == JsonValueKind.Array => ValidationResult.Success,
                JsonElement json when json.ValueKind == JsonValueKind.String => ValidationResult.Success,
                JsonElement json when json.ValueKind == JsonValueKind.Number => ValidationResult.Success,
                _ => new ValidationResult(errorMessage)
            };
        }
    }
}

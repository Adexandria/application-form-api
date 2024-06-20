using FormAPI.Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Models.DTOs.ResponseDTOs
{
    /// <summary>
    /// Manages response creation
    /// </summary>
    public class CreateResponseDto
    {

        /// <summary>
        /// Question id
        /// </summary>
        [Required(ErrorMessage = "Invalid Question id")]
        public string QuestionId { get; set; }

        /// <summary>
        /// Response of the question
        /// </summary>
        [ResponseType("Invalid response type")]
        public object Response { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;

namespace FormAPI.Models.DTOs.FormDTOs
{
    /// <summary>
    /// Manages form creation
    /// </summary>
    public class CreateFormDto
    {

        /// <summary>
        /// Title of the form
        /// </summary>
        [Required(ErrorMessage = "Invalid title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of form
        /// </summary>
        [Required(ErrorMessage = "Invalid description")]
        public string Description { get; set; }

        /// <summary>
        /// A list of questions
        /// </summary>
        public List<CreateQuestionDto> Questions { get; set; }
    }
}

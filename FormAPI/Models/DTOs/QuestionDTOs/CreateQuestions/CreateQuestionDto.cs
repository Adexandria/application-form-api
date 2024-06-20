using FormAPI.Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions
{
    /// <summary>
    /// Model to manage question creation
    /// </summary>
    public class CreateQuestionDto
    {
        /// <summary>
        /// Question type
        /// </summary>
        [Allow("Invalid question type", "Paragraph", "YesNo", "DropDown", "Multichoice", "Number", "Date")]
        public string QuestionType { get; set; }

        /// <summary>
        /// Content of the question
        /// </summary>
        [Required(ErrorMessage = "Invalid question content")]
        public string Content { get; set; }

        /// <summary>
        /// Choices of the question (optional)
        /// </summary>
        public List<CreateChoiceDto> Choices { get; set; }

        /// <summary>
        ///  Max choices in a multi-choice question
        /// </summary>
        public int MaxChoices { get; set; }

        /// <summary>
        /// Question arrangement e.g 1
        /// </summary>
        /// 
        [IsValidInteger("Invalid question number",1)]
        public int QuestionNumber { get; set; }

        /// <summary>
        /// Group of the question e.g personal information
        /// </summary>
        [Allow("Invalid question group", "PersonalInformation", "CustomInformation")]
        public string QuestionGroup { get; set; }
    }
}

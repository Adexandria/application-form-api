

namespace FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions
{
    /// <summary>
    /// Model to manage choice creationss
    /// </summary>
    public class CreateChoiceDto
    {
        /// <summary>
        /// Content of choice
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Choice arrangement e.g 1
        /// </summary>
        public int ChoiceNumber { get; set; }
    }
}

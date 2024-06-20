namespace FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions
{
    /// <summary>
    /// Model to update existing question
    /// </summary>
    public class UpdateQuestionDto
    {
        /// <summary>
        /// Content of question
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///  Choices of the question (optional)
        /// </summary>
        public List<UpdateChoiceDto> Choices { get; set; }

        /// <summary>
        /// Number of choices allowed e.g 3
        /// </summary>
        public int MaxChoices { get; set; }
    }
}

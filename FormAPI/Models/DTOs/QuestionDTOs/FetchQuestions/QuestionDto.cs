namespace FormAPI.Models.DTOs.QuestionDTOs.FetchQuestions
{
    /// <summary>
    /// Manages how a question will be displayed
    /// </summary>
    public class QuestionDto
    {
        /// <summary>
        /// A constructor
        /// </summary>
        public QuestionDto()
        {
                
        }
        public QuestionDto(string content, string questionId)
        {
            Content = content;
            QuestionId = questionId;
        }

        /// <summary>
        /// Content of the question
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Question id
        /// </summary>
        public string QuestionId { get; set; }

        /// <param name="content">Content of the question</param>
        /// <param name="questionId">Question id</param>
       
    }
}

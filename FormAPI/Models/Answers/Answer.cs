namespace FormAPI.Models.Answers
{
    /// <summary>
    /// Model to manage answer
    /// </summary>
    /// <param name="questionId">Question id</param>
    public class Answer(string questionId): BaseClass()
    {
        /// <summary>
        /// Question id
        /// </summary>
        public string QuestionId { get; set; } = questionId;       
    }
}

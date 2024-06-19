namespace FormAPI.Models.Answers
{
    /// <summary>
    /// Handles yes or no submitted responses
    /// </summary>
    /// <param name="response">A boolean response</param>
    /// <param name="questionId">Question id</param>
    public class YesNoSubmittedAnswer(bool response, string questionId): Answer(questionId)
    {
        /// <summary>
        /// Response of the question
        /// </summary>
        public int Response {  get; set; } = Convert.ToInt32(response);
    }
}

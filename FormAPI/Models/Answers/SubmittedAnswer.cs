namespace FormAPI.Models.Answers
{
     /// <summary>
     /// Model that manages submitted responses
     /// </summary>
    public class SubmittedAnswer : Answer
    {
        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="response">Response of question</param>
        /// <param name="questionId">Question id</param>
        public SubmittedAnswer(string response, string questionId) : base(questionId)
        {
            Response = response;
        }


        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="response">Response of question</param>
        /// <param name="questionId">Question id</param>
        public SubmittedAnswer(int response, string questionId) : base(questionId)
        {
            Response = response.ToString();
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="response">Response of question</param>
        /// <param name="questionId">Question id</param>
        public SubmittedAnswer(DateTime response, string questionId) : base(questionId)
        {
            Response = response.ToString("s");
        }

        /// <summary>
        /// Response of the question
        /// </summary>
        public string Response { get; set; }
    }
}

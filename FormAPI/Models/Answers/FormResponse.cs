namespace FormAPI.Models.Answers
{
    /// <summary>
    /// Model that manages form responses
    /// </summary>
    /// <param name="formId">Form id</param>
    public class FormResponse(string formId): BaseClass() 
    {
        /// <summary>
        /// Adds new response to existing response
        /// </summary>
        /// <param name="answer">New response</param>
        public void AddResponse(Answer answer)
        {
             Responses.Add(answer);   
        }

        /// <summary>
        /// Form id
        /// </summary>
        public string FormId { get; set; } = formId;

        /// <summary>
        /// List of responses
        /// </summary>
        public IList<Answer> Responses { get; set; } = [];
    }
}

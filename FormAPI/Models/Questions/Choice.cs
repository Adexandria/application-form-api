namespace FormAPI.Models.Questions
{
    /// <summary>
    /// Manages choice
    /// </summary>
    /// <param name="content">Content of choice</param>
    /// <param name="choiceNumber">Choice number</param>
    public class Choice(string content, int choiceNumber) : BaseClass()
    {
        /// <summary>
        /// Content of choice
        /// </summary>
        public string Content { get; set; } = content;

        /// <summary>
        /// Choice number
        /// </summary>
        public string ChoiceNumber { get; set; } = choiceNumber.ToString();
    }
}

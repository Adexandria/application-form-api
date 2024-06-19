using FormAPI.Models.Questions;

namespace FormAPI.Infrastructure.Utility
{
    /// <summary>
    /// Model used to fetch choices from container
    /// </summary>
    public class QuestionChoice
    {
        /// <summary>
        /// List of choices
        /// </summary>
        public List<Choice> Choices { get; set; }

        /// <summary>
        /// Maximum number of choices
        /// </summary>
        public string MaxChoices { get; set; }

        /// <summary>
        /// Question id
        /// </summary>

        public string id {  get; set; }
    }
}

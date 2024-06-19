using System.Text.Json.Nodes;

namespace FormAPI.Models.Answers
{
    /// <summary>
    /// Manages multichoice submitted responses
    /// </summary>
    /// <param name="questionId">Question id</param>
    public class MultiChoiceSubmittedAnswer(string questionId) : Answer(questionId)
    {

        /// <summary>
        /// Add choice ids
        /// </summary>
        /// <param name="array">Json array</param>
        /// <returns>Multichoice submitted answer</returns>
        public MultiChoiceSubmittedAnswer AddChoiceIds(JsonArray array)
        {
            foreach (var item in array)
            {
                ChoiceIds.Add(item.ToString());
            }

            return this;
        }

        /// <summary>
        /// A list of choice ids
        /// </summary>
        public List<string> ChoiceIds { get; set; } = [];
    }
}

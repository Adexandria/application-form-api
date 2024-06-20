using FormAPI.Models.Questions;

namespace FormAPI.Models.DTOs.QuestionDTOs.FetchQuestions
{
    /// <summary>
    /// Manages how a multi choic question will be displayed
    /// </summary>
    public class MultiChoiceQuestionDto : QuestionDto
    {

        /// <summary>
        /// A constructor
        /// </summary>
        public MultiChoiceQuestionDto()
        {
                
        }


        /// <param name="content">Content of the question</param>
        public MultiChoiceQuestionDto(string content, string questionId) : base(content, questionId)
        {

        }
        /// <summary>
        /// Add choices with number of maximum choices
        /// </summary>
        /// <param name="choices">Add existing choices</param>
        /// <param name="maxChoices">Maximum number of choices</param>
        /// <returns>Multichoice question</returns>
        public MultiChoiceQuestionDto AddChoices(List<Choice> choices, string maxChoices
            )
        {
            Choices = choices
             .OrderBy(s => s.ChoiceNumber)
             .Select(choice => new ChoiceDto
             {
                 Content = choice.Content,
                 Id = choice.Id
             }).ToList();

            MaxChoices = maxChoices;

            return this;
        }


        /// <summary>
        /// Maximum number of choices
        /// </summary>
        public string MaxChoices { get; set; }

        /// <summary>
        /// A list of choices
        /// </summary>
        public IList<ChoiceDto> Choices { get; set; }

        
    }
}

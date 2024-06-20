using FormAPI.Models.Questions;

namespace FormAPI.Models.DTOs.QuestionDTOs.FetchQuestions
{
    /// <summary>
    /// Manages how drop down question will be displayed
    /// </summary>
    public class DropDownQuestionDto : QuestionDto
    {

        /// <param name="content">Content of the question</param>
        public DropDownQuestionDto(string content, string questionId) : base(content, questionId)
        {
        }

        /// <summary>
        /// A constructor
        /// </summary>
        public DropDownQuestionDto():base()
        {
               
        }
        /// <summary>
        /// Add new choices
        /// </summary>
        /// <param name="choices">Choices of the question</param>
        /// <returns>Drop down question</returns>
        public DropDownQuestionDto AddChoices(List<Choice> choices)
        {
            Choices = choices
            .OrderBy(s => s.ChoiceNumber).Select(choice => new ChoiceDto 
            {
                Content = choice.Content,
                Id = choice.Id
                
            }).ToList();

            return this;
        }

        /// <summary>
        /// Choices of the question
        /// </summary>
        public IList<ChoiceDto> Choices { get; set; }

        
    }
}

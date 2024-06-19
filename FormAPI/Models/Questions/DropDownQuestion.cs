using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.Enums;

namespace FormAPI.Models.Questions
{
    /// <summary>
    /// Model used to manage drop down question
    /// </summary>
    public class DropDownQuestion : Question
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DropDownQuestion() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="content">Content of the question</param>
        /// <param name="questionNumber">Arrangement of the question</param>
        /// <param name="questionGroup">Group of the question</param>
        public DropDownQuestion(string formId, string content, int questionNumber, string questionGroup) 
            : base(formId, content, questionNumber, questionGroup)
        {
        }


        /// <summary>
        /// Add a list of choices
        /// </summary>
        /// <param name="choices">A list of choice to add</param>
        /// <returns>Drop down question</returns>
        public DropDownQuestion AddChoices(List<CreateChoiceDto> choices)
        {
            Choices = choices.Select(choice => new Choice(choice.Content, choice.ChoiceNumber)).ToList();
            return this;
        }


        /// <summary>
        /// Update existing choices
        /// </summary>
        /// <param name="updateChoices">A list of update choices</param>
        /// <returns>Drop down question</returns>
        public DropDownQuestion UpdateChoices(List<UpdateChoiceDto> updateChoices)
        {
            Choices = updateChoices.Select(choice => new Choice(choice.Content, choice.ChoiceNumber)).ToList();
            return this;
        }

        /// <summary>
        /// Update existing Content
        /// </summary>
        /// <param name="content">Content of question</param>
        /// <returns>Drop down question</returns>
        public new DropDownQuestion UpdateContent(string content)
        {
            Content = content;
            return this;
        }

        /// <summary>
        /// Private setter field 
        /// </summary>
        private QuestionType questionType = QuestionType.DropDown;

        /// <summary>
        /// Question type
        /// </summary>
        public override QuestionType QuestionType { get => questionType; set => questionType = value; }

        /// <summary>
        /// List of choices
        /// </summary>
        public IList<Choice> Choices { get; private set; }
    }
}

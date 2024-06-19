using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.Enums;

namespace FormAPI.Models.Questions
{
    /// <summary>
    /// A model to manage multi choice question
    /// </summary>
    public class MultiChoiceQuestion : Question
    {
        /// <summary>
        /// A constructor
        /// </summary>
        public MultiChoiceQuestion()
        {
            
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="content">Content of the question</param>
        /// <param name="questionNumber">Arrangement of the question</param>
        /// <param name="questionGroup">Group of the question</param>
        public MultiChoiceQuestion(string formId, string content, int questionNumber,
           string questionGroup) : base(formId, content, questionNumber, questionGroup)
        {
        }

        /// <summary>
        /// Add list of choices and maximum number of choices
        /// </summary>
        /// <param name="choices">List of choices</param>
        /// <param name="maxChoices">Maximum number of choices</param>
        /// <returns>Multi Choice</returns>
        public MultiChoiceQuestion AddChoices(List<CreateChoiceDto> choices, int maxChoices)
        {
            Choices = choices.Select(choice => new Choice(choice.Content,choice.ChoiceNumber)).ToList();
            MaxChoices = maxChoices.ToString();
            return this;
        }

        /// <summary>
        /// Update existing choices
        /// </summary>
        /// <param name="updateChoices">A list of update choices</param>
        /// <param name="maxChoices">Maximum number of choices</param>
        /// <returns>Multi choice question</returns>
        public MultiChoiceQuestion UpdateChoices(List<UpdateChoiceDto> updateChoices, int maxChoices)
        {
            Choices = updateChoices.Select(choice => new Choice(choice.Content, choice.ChoiceNumber)).ToList();
            MaxChoices = maxChoices.ToString();
            return this;
        }

        /// <summary>
        /// Update existing content
        /// </summary>
        /// <param name="content">Update content</param>
        /// <returns>Multi choice question</returns>
        public new MultiChoiceQuestion UpdateContent(string content)
        {
            Content = content;
            return this;
        }

        /// <summary>
        /// Maximum number of choices
        /// </summary>
        public string MaxChoices { get; private set; }

        /// <summary>
        /// A list of choices
        /// </summary>
        public IList<Choice> Choices { get; private set; }

        /// <summary>
        /// Question type
        /// </summary>
        public override QuestionType QuestionType { get => questionType; set => questionType = value; }

        /// <summary>
        /// Private field to set question type
        /// </summary>

        private QuestionType questionType = QuestionType.Multichoice;
    }
}

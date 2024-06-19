using FormAPI.Models.Enums;

namespace FormAPI.Models.Questions
{
    /// <summary>
    /// A model to manage questions
    /// </summary>
    public class Question : BaseClass
    {
        /// <summary>
        /// A constructor
        /// </summary>
        protected Question()
        {
           
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="content">Content of the question</param>
        /// <param name="questionNumber">Arrangement of the question</param>
        /// <param name="questionGroup">Group of the question</param>
        public Question(string formId,string content, int questionNumber,
            string questionGroup):base()
        {
            FormId = formId;
            Content = content;
            QuestionNumber = questionNumber.ToString();
            QuestionGroup = Enum.Parse<QuestionGroup>(questionGroup);
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="content">Content of the question</param>
        /// <param name="questionNumber">Arrangement of the question</param>
        /// <param name="questionGroup">Group of the question</param>
        /// <param name="questionType">Question type</param>
        public Question(string formId, string content, int questionNumber 
            ,string questionGroup, string questionType) : base()
        {
            FormId = formId;
            Content = content;
            QuestionNumber = questionNumber.ToString();
            QuestionGroup = Enum.Parse<QuestionGroup>(questionGroup);
            QuestionType = Enum.Parse<QuestionType>(questionType);
        }

        /// <summary>
        /// Updates existing content
        /// </summary>
        /// <param name="content">Content of question</param>
        /// <returns>Question</returns>
        public Question UpdateContent(string content)
        {
            Content = content;
            return this;
        }

        /// <summary>
        /// Form id of question
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Content of question
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Number order of question
        /// </summary>
        public string QuestionNumber { get; set; }

        /// <summary>
        /// Question Group
        /// </summary>
        public QuestionGroup QuestionGroup { get; set; }

        /// <summary>
        /// Question type
        /// </summary>
        public virtual QuestionType QuestionType { get; set; }
    }
}

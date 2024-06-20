using FormAPI.Infrastructure.Utility;
using FormAPI.Infrastructure.Validations;
using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.FetchQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.Questions;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// Manages question operations
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Creates new question
        /// </summary>
        /// <param name="formId">Form Id</param>
        /// <param name="newQuestion">A model to manage question creation</param>
        /// <returns>Response result</returns>
        Task<ResponseResult> CreateQuestion( string formId,
            CreateQuestionDto newQuestion);


        /// <summary>
        /// Updates an existing question
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <param name="formId">Form id</param>
        /// <param name="updateQuestion">A model used to update questions</param>
        /// <returns>Response result</returns>
        Task<ResponseResult> UpdateQuestion(string questionId,string formId,UpdateQuestionDto updateQuestion);

        /// <summary>
        /// Fetches questions by type
        /// </summary>
        /// <param name="questionType">Question type</param>
        /// <param name="formId">Form id</param>
        /// <returns>List of question</returns>
        Task<ResponseResult<List<Question>>> FetchQuestionsByType( string formId, string questionType);

        /// <summary>
        /// fetch choices by type
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="questionType">Question id</param>
        /// <returns>A list of choices</returns>
        Task<ResponseResult<List<QuestionChoice>>> FetchQuestionChoice(string formId, string questionType);
    }
}

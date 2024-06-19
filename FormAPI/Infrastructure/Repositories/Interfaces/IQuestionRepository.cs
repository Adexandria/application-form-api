using FormAPI.Infrastructure.Utility;
using FormAPI.Models.Enums;
using FormAPI.Models.Questions;

namespace FormAPI.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Manages question container
    /// </summary>
    public interface IQuestionRepository : IRepository<Question>
    {

        /// <summary>
        /// Fetches a list of question using question type
        /// </summary>
        /// <param name="questionType">Question type</param>
        /// <param name="formId">Form id</param>
        /// <returns>A list of questions</returns>
        Task<List<Question>> GetQuestions(string formId, QuestionType questionType);


        /// <summary>
        /// Fetches choice in a question
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="questionType">Question type</param>
        /// <returns>A list of choices</returns>
        Task<List<QuestionChoice>> GetQuestionChoices(string formId,QuestionType questionType);

        /// <summary>
        /// Fetches a single question using the id and partition key
        /// </summary>
        /// <param name="id">Question id</param>
        /// <param name="partitionKey">Partition key</param>
        /// <returns>A question</returns>
        Task<Question> GetQuestion(string id, string partitionKey);

        /// <summary>
        /// Verifies if question exists
        /// </summary>
        /// <param name="id">Question id</param>
        /// <param name="partitionKey">Partition key</param>
        /// <returns>boolean value</returns>
        Task<bool> IsExist(string id, string partitionKey);

        /// <summary>
        /// Updates an existing question in a container
        /// </summary>
        /// <param name="item">Manages model</param>
        /// <returns>A boolean value to denote if it was successfully</returns>
        Task<bool> UpdateQuestion(Question item);
    }
}

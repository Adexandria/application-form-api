using FormAPI.Infrastructure.Extensions;
using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Services.Interfaces;
using FormAPI.Infrastructure.Utility;
using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.Enums;
using FormAPI.Models.Questions;

namespace FormAPI.Infrastructure.Services
{
    /// <summary>
    /// Manages question operations
    /// </summary>
    /// <param name="questionRepository">Manages question container</param>
    /// <param name="formRepository">Manages form container</param>
    public class QuestionService(IQuestionRepository questionRepository, IFormRepository formRepository) : BaseService, IQuestionService
    {
        /// <summary>
        /// Updates an existing question
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <param name="formId">Form id</param>
        /// <param name="updateQuestion">A model used to update questions</param>
        /// <returns>Response result</returns>
        public async Task<ResponseResult> UpdateQuestion(string questionId, string formId, UpdateQuestionDto updateQuestion)
        {
            var isExist = await _formRepository.IsExist(formId);
            if(!isExist)
            {
                return FailedOperation("Form does not exist", 404);
            }
            var currentQuestion = await _questionRepository.GetQuestion(questionId, formId);
            if (currentQuestion == null)
            {
                return FailedOperation("Question does not exist", 404);
            }

           var updatedQuestion =  currentQuestion.UpdateQuestionExistingBasedOnType(updateQuestion);

            var response = await _questionRepository.UpdateQuestion(updatedQuestion);

            if (!response)
            {
                return FailedOperation("Failed to update question");
            }

            return SuccessfulOperation();

        }

        /// <summary>
        /// fetches questions by type
        /// </summary>
        /// <param name="questionType">Question type</param>
        /// <param name="formId">Form id</param>
        /// <returns>A list of question</returns>
        public async Task<ResponseResult<List<Question>>> FetchQuestionsByType(string formId, string questionType)
        {
            var type = Enum.Parse<QuestionType>(questionType);
            var currentQuestions = await _questionRepository.GetQuestions(formId, type);

            if (!currentQuestions.Any())
            {
                return FailedOperation<List<Question>>("There are no questions",404);
            }

            return SuccesfulOperation(currentQuestions);
        }

        /// <summary>
        /// fetch choices by type
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="questionType">Question id</param>
        /// <returns>A list of choices</returns>
        public async Task<ResponseResult<List<QuestionChoice>>> FetchQuestionChoice(string formId, string questionType)
        {
            var choices = await _questionRepository.GetQuestionChoices(formId,Enum.Parse<QuestionType>(questionType));

            return SuccesfulOperation(choices);
        }



        /// <summary>
        /// Creates new question
        /// </summary>
        /// <param name="formId">Form Id</param>
        /// <param name="newQuestion">A model to manage question creation</param>
        /// <returns>Response result</returns>
        public async Task<ResponseResult> CreateQuestion(string formId, CreateQuestionDto newQuestion)
        {
            var isExist = await _formRepository.IsExist(formId);
            if (!isExist)
            {
                return FailedOperation("Form does not exist", 404);
            }
            //Convert question to it's model (model managing the question type)
            var question = newQuestion.CreateQuestionBasedOnType(formId);

            //Persists data to the container
            var questionResponse = await _questionRepository.AddItem(question);

            if (!questionResponse)
            {
                return FailedOperation("Failed to add question");
            }

            return SuccessfulOperation(201);
            
        }

        /// <summary>
        /// Manages question container
        /// </summary>
        private readonly IQuestionRepository _questionRepository = questionRepository;

        /// <summary>
        /// Manages form container
        /// </summary>

        private readonly IFormRepository _formRepository = formRepository;
    }
}

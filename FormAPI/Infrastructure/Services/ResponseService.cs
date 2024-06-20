using FormAPI.Infrastructure.Extensions;
using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Services.Interfaces;
using FormAPI.Infrastructure.Utility;
using FormAPI.Models.Answers;
using FormAPI.Models.DTOs.ResponseDTOs;

namespace FormAPI.Infrastructure.Services
{
    /// <summary>
    /// Manages response operations
    /// </summary>
    /// <param name="responseRepository">Handle response container</param>
    /// <param name="questionRepository">Handle question container</param>
    public class ResponseService(IResponseRepository responseRepository,
        IQuestionRepository questionRepository, IFormRepository formRepository) : BaseService,IResponseService
    {

        /// <summary>
        /// Submit responses to existing form
        /// </summary>
        /// <param name="newResponses">A model to create new responses</param>
        /// <param name="formId">Form id</param>
        /// <returns>Response result</returns>

        public async Task<ResponseResult> SubmitResponse(string formId, List<CreateResponseDto> newResponses)
        {
            var isFormExist = await _formRepository.IsExist(formId);    

            if(!isFormExist)
            {
                return FailedOperation("form doesn't exist", 404);
            }

            var questionResponse = new FormResponse(formId);

            foreach(var newResponse in newResponses)
            {
                var isQuestionExist = await _questionRepository.IsExist(newResponse.QuestionId, formId);

                if(!isQuestionExist)
                {
                    return FailedOperation("Question doesn't exist", 404);
                }

                var response = newResponse.CreateReponsesBasedOnQuestionType();

                if(response == null)
                {
                    return FailedOperation("Invalid response");
                }

                questionResponse.AddResponse(response);
            }

            var result = await _responseRepository.AddItem(questionResponse);

            if (!result)
            {
                return FailedOperation("Failed to add response");
            }

            return SuccessfulOperation();
        }

        /// <summary>
        /// Handles question container
        /// </summary>
        private readonly IQuestionRepository _questionRepository = questionRepository;

        /// <summary>
        /// Handles response container
        /// </summary>
        private readonly IResponseRepository _responseRepository = responseRepository;

        /// <summary>
        /// Handles form container
        /// </summary>
        private readonly IFormRepository _formRepository = formRepository;
    }
}

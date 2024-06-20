using FormAPI.Infrastructure.Utility;
using FormAPI.Models.DTOs.ResponseDTOs;

namespace FormAPI.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// Manages response operations
    /// </summary>
    public interface IResponseService
    {
        /// <summary>
        /// Submit responses to existing form
        /// </summary>
        /// <param name="newResponses">A model to create new responses</param>
        /// <param name="formId">Form id</param>
        /// <returns>Response result</returns>
        Task<ResponseResult> SubmitResponse(string formId, List<CreateResponseDto> newResponses);
    }
}

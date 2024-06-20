using FormAPI.Infrastructure.Utility;
using FormAPI.Models.DTOs.FormDTOs;

namespace FormAPI.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// Handles form operations
    /// </summary>
    public interface IFormService
    {

        /// <summary>
        /// Creates new form
        /// </summary>
        /// <param name="newForm">Manages form creation</param>
        /// <returns>Response result</returns>
        Task<ResponseResult> CreateForm(CreateFormDto newForm);

        /// <summary>
        /// Updates existing form
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="updateForm">Manages  form updates </param>
        /// <returns>Response result</returns>
    }
}

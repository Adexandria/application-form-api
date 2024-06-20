using FormAPI.Infrastructure.Extensions;
using FormAPI.Infrastructure.Services.Interfaces;
using FormAPI.Models.DTOs.FormDTOs;
using FormAPI.Models.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Controllers
{
    /// <summary>
    /// Manages application form endpoints
    /// </summary>
    /// <param name="formService">Handles form operations</param>
    /// <param name="responseService">Handles response operations</param>
    [Route("api/application-forms")]
    [ApiController]
    public class ApplicationFormController(IFormService formService, IResponseService responseService) : ControllerBase
    {
        /// <summary>
        /// Creates a new form
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///        POST /application-forms        
        /// 
        /// </remarks>
        /// <param name="newForm">A model used to create new form</param>
        /// <response code ="200"> Returns if form was created successfully</response>
        /// <response code ="400"> Returns if failed to create form</response>
        /// <response code ="500"> Returns if experiencing server issues</response>
        /// <returns>Action result</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateForm(CreateFormDto newForm)
        {
            var response = await _formService.CreateForm(newForm);

            return response.Response();
        }



        /// <summary>
        /// Submits responses to an existing form
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///        POST /application-forms/f48575116f8e49ffb0c5b44994e27397/submit     
        /// 
        /// </remarks>
        /// <param name="formId">Form id</param>
        /// <param name="newResponses">Model to create new responses</param>
        /// <response code ="201"> Returns if form was submitted successfully</response>
        /// <response code ="400"> Returns if failed to submit form</response>
        /// <response code ="404"> Returns if form/question does not exist</response>
        /// <response code ="500"> Returns if experiencing server issues</response>
        /// <returns>Action result</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [HttpPost("{formId}/submit")]
        public async Task<IActionResult> SubmitForm([Required(ErrorMessage = "Invalid form id")] string formId, List<CreateResponseDto> newResponses)
        {
            var response = await _responseService.SubmitResponse(formId, newResponses);
            
            return response.Response();
        }

        /// <summary>
        /// Handles form operations
        /// </summary>
        private readonly IFormService _formService = formService;

        /// <summary>
        /// Handles response operations
        /// </summary>
        private readonly IResponseService _responseService = responseService;
    }
}

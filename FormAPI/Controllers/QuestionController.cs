using FormAPI.Infrastructure.Extensions;
using FormAPI.Infrastructure.Services.Interfaces;
using FormAPI.Infrastructure.Validations;
using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.FetchQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Controllers
{
    /// <summary>
    /// Manages question endpoints
    /// </summary>
    /// <param name="questionService">Manages question operations</param>
    [Route("api/{formId}/questions")]
    [ApiController]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        /// <summary>
        /// Creates a new question
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///        POST /f48575116f8e49ffb0c5b44994e27397/questions       
        /// 
        /// </remarks>
        /// <param name="formId">Form id</param>
        /// <param name="newQuestion">Model used to create question</param>
        /// <response code ="201"> Returns if question was created successfully</response>
        /// <response code ="400"> Returns if failed to create question</response>
        /// <response code ="500"> Returns if experiencing server issues</response>
        /// <returns>Action result</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateNewQuestion([Required(ErrorMessage = "Invalid form id")] string formId, CreateQuestionDto newQuestion)
        {
            var response = await _questionService.CreateQuestion(formId, newQuestion);

            return response.Response();
        }


        /// <summary>
        /// Updates an existing question
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///        PUT /f48575116f8e49ffb0c5b44994e27397/questions/f48575116f8e49ffb0c5b44994e27398   
        /// 
        /// </remarks>
        /// <param name="formId">Form id</param>
        /// <param name="questionId">Question id</param>
        /// <param name="updateQuestion">Model to update an existing question</param>
        /// <response code ="200"> Returns if question was updated successfully</response>
        /// <response code ="400"> Returns if failed to update question</response>
        /// <response code ="404"> Returns if form doesn't exist</response>
        /// <response code ="500"> Returns if experiencing server issues</response>
        /// <returns>Action result</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [HttpPut("{questionId}")]
        public async Task<IActionResult> UpdateQuestion([Required(ErrorMessage = "Invalid question id")] string formId,
            [Required(ErrorMessage = "Invalid form id")] string questionId, UpdateQuestionDto updateQuestion)
        {
            var response = await _questionService.UpdateQuestion(questionId, formId, updateQuestion);

            return response.Response();
        }


        /// <summary>
        /// Gets question by question type
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///        GET /f48575116f8e49ffb0c5b44994e27397/questions?questionType=YesNo    
        /// 
        /// </remarks>
        /// <param name="formId">Form id</param>
        /// <param name="questionType">Type of the question</param>
        /// <response code ="200"> Returns if questions exists</response>
        /// <response code ="404"> Returns if no questions exist</response>
        /// <response code ="500"> Returns if experiencing server issues</response>
        /// <returns>Action result</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> FetchQuestionsbyType([Required(ErrorMessage = "Invalid form id")] string formId,
                       [Allow("Invalid question type", "Paragraph", "YesNo", "DropDown", "Multichoice", "Number", "Date")] string questionType)
        {
            var response = await _questionService.FetchQuestionsByType(formId, questionType);
            if(response.StatusCode != 200)
            {
                return response.Response();
            }

            var questionChoices = await _questionService.FetchQuestionChoice(formId, questionType);

            return questionType switch
            {
                 _ when QuestionType.Multichoice.ToString() == questionType =>
                    Ok(response.Data.CreateQuestionsDtosBasedOnType<MultiChoiceQuestionDto>(questionChoices.Data)),

            _ when QuestionType.DropDown.ToString() == questionType =>
               Ok(response.Data.CreateQuestionsDtosBasedOnType<MultiChoiceQuestionDto>(questionChoices.Data)),

            _ => Ok(response.Data.CreateQuestionsDtosBasedOnType<QuestionDto>(questionChoices.Data))
            };
        }

        /// <summary>
        /// Handles question operations
        /// </summary>
        private readonly IQuestionService _questionService = questionService;
    }

}

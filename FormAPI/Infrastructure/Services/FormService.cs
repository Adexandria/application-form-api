using FormAPI.Infrastructure.Extensions;
using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Services.Interfaces;
using FormAPI.Infrastructure.Utility;
using FormAPI.Models.DTOs.FormDTOs;
using FormAPI.Models.Forms;

namespace FormAPI.Infrastructure.Services
{
    /// <summary>
    /// Handles form operations
    /// </summary>
    /// <param name="formRepository">Manages form container</param>
    /// <param name="questionRepository">Manges question container</param>
    public class FormService(IFormRepository formRepository, IQuestionRepository questionRepository) : BaseService,IFormService
    {
        /// <summary>
        /// Creates new form
        /// </summary>
        /// <param name="newForm">Manages form creation</param>
        /// <returns>Response result</returns>
        public async Task<ResponseResult> CreateForm(CreateFormDto newForm)
        {
            var form = new Form(newForm.Title, newForm.Description);

            //Adds new form to container
            var response = await _formRepository.AddItem(form);

            if (!response)
            {
                return FailedOperation("Failed to add form");
            }

            foreach(var newQuestion in newForm.Questions)
            {
                //Convert question to it's model (model managing the question type)
                var question = newQuestion.CreateQuestionBasedOnType(form.Id);

                //Persists data to the container
                var questionResponse = await _questionRepository.AddItem(question);

                // If it's fails do not persist again
                if (!questionResponse)
                {
                    return FailedOperation("Failed to add question");      
                }
            }

            return SuccessfulOperation(201);
        }


        /// <summary>
        /// Manages form container
        /// </summary>
        private readonly IFormRepository _formRepository = formRepository;

        /// <summary>
        /// Manages question container
        /// </summary>
        private readonly IQuestionRepository _questionRepository = questionRepository;

    }
}

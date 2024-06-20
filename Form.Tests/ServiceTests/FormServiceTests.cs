using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Services;
using FormAPI.Models.DTOs.FormDTOs;
using FormAPI.Models.Questions;
using Moq;


namespace Form.Tests.ServiceTests
{
    public class FormServiceTests
    {

        [Fact]
        public async Task ShouldCreateFormSuccessfully()
        {
          Mock<IQuestionRepository> _questionRepositoryMock = new();
          Mock<IFormRepository> _formRepositoryMock = new();

          _formRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<FormAPI.Models.Forms.Form>()))
                .ReturnsAsync(true);

            _questionRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<Question>())).ReturnsAsync(true);

            var formService = new FormService(_formRepositoryMock.Object, _questionRepositoryMock.Object);

            var response = await formService.CreateForm(new CreateFormDto() 
            { 
                Description ="Test description",
                Title= "Title",
                Questions =
                [
                  new() {
                     Content="Enter test value",
                     QuestionNumber= 1,
                     QuestionGroup="PersonalInformation",
                     QuestionType= "Paragraph"
                  }   
                ]
            });

            Assert.True(response.StatusCode == 201);
        }


        [Fact]
        public async Task ShouldFailedToCreateFormWithoutQuestionSuccessfully()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<FormAPI.Models.Forms.Form>()))
                  .ReturnsAsync(false);

            var formService = new FormService(_formRepositoryMock.Object, _questionRepositoryMock.Object);

            var response = await formService.CreateForm(new CreateFormDto()
            {
                Description = "Test description",
                Title = "Title",
                Questions =
                [
                  new() {
                     Content="Enter test value",
                     QuestionNumber= 1,
                     QuestionGroup="PersonalInformation",
                     QuestionType= "Paragraph"
                  }
                ]
            });

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public async Task ShouldFailedToCreateFormWithQuestions()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<FormAPI.Models.Forms.Form>()))
                  .ReturnsAsync(true);

            _questionRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<Question>())).ReturnsAsync(false);

            var formService = new FormService(_formRepositoryMock.Object, _questionRepositoryMock.Object);

            var response = await formService.CreateForm(new CreateFormDto()
            {
                Description = "Test description",
                Title = "Title",
                Questions =
                [
                  new() {
                     Content="Enter test value",
                     QuestionNumber= 1,
                     QuestionGroup="PersonalInformation",
                     QuestionType= "Paragraph"
                  }
                ]
            });

            Assert.True(response.StatusCode == 400);
        }
    }
}

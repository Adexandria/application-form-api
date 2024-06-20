using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Services;
using FormAPI.Infrastructure.Utility;
using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.Enums;
using FormAPI.Models.Questions;
using Moq;

namespace Form.Tests.ServiceTests
{
    public class QuestionServiceTests
    {
        [Fact]
        public async Task ShouldCreateQuestionSuccessfully()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<Question>())).ReturnsAsync(true);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.CreateQuestion("12345", new CreateQuestionDto
            {
                Content = "Enter test value",
                QuestionNumber = 1,
                QuestionGroup = "PersonalInformation",
                QuestionType = "Paragraph"
            });

            Assert.True(response.StatusCode == 201);
        }

        [Fact]
        public async Task ShouldFailtoCreateQuestionIfFormDoNotExist()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(false);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.CreateQuestion("12345", new CreateQuestionDto
            {
                Content = "Enter test value",
                QuestionNumber = 1,
                QuestionGroup = "PersonalInformation",
                QuestionType = "Paragraph"
            });

            Assert.True(response.StatusCode == 404);
        }

        [Fact]
        public async Task ShouldFailtoCreateQuestionIfCommitFail()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(true);
            _questionRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<Question>()))
                .ReturnsAsync(false);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.CreateQuestion("12345", new CreateQuestionDto
            {
                Content = "Enter test value",
                QuestionNumber = 1,
                QuestionGroup = "PersonalInformation",
                QuestionType = "Paragraph"
            });

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public async Task ShouldFetchQuestionChoicesSuccesfully()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();

            Mock<IFormRepository> _formRepositoryMock = new();

            _questionRepositoryMock.Setup(repo => repo
            .GetQuestionChoices(It.IsAny<string>(), It.IsAny<QuestionType>()))
                .ReturnsAsync(new List<QuestionChoice>
                {
                    new QuestionChoice()
                });

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.FetchQuestionChoice("123", "Paragraph");

            Assert.True(response.StatusCode == 200);
        }

        [Fact]
        public async Task ShouldFetchQuestionsByType()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();

            Mock<IFormRepository> _formRepositoryMock = new();

            _questionRepositoryMock.Setup(repo => repo
            .GetQuestions(It.IsAny<string>(), It.IsAny<QuestionType>()))
                .ReturnsAsync(
                [
                    new("12","Content",1,"CustomInformation","Paragraph")
                ]) ;

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.FetchQuestionChoice("123", "Paragraph");

            Assert.True(response.StatusCode == 200);
        }


        [Fact]
        public async Task ShouldFailtoFetchQuestionsByType()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();

            Mock<IFormRepository> _formRepositoryMock = new();

            _questionRepositoryMock.Setup(repo => repo
            .GetQuestions(It.IsAny<string>(), It.IsAny<QuestionType>()))
                .ReturnsAsync([]);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.FetchQuestionsByType("123", "Paragraph");

            Assert.True(response.StatusCode == 404);
        }

        [Fact]
        public async Task ShouldUpdateQuestionSuccessfully()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(true);

            _questionRepositoryMock.Setup(repo => repo.GetQuestion(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Question("12", "Content", 1, "CustomInformation", "Paragraph"));

            _questionRepositoryMock.Setup(repo => repo.UpdateQuestion(It.IsAny<Question>())).ReturnsAsync(true);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.UpdateQuestion("12345", "12", new UpdateQuestionDto
            {
                Content = "Enter test value"
            });

            Assert.True(response.StatusCode == 200);
        }

        [Fact]
        public async Task ShouldFailToUpdateQuestionIfFormDoNotExist()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(false);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.UpdateQuestion("12345", "12", new UpdateQuestionDto
            {
                Content = "Enter test value"
            });

            Assert.True(response.StatusCode == 404);
        }

        [Fact]
        public async Task ShouldFailToUpdateQuestionIfQuestionDoesNotExist()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(true);

            _questionRepositoryMock.Setup(repo => repo.GetQuestion(It.IsAny<string>(), It.IsAny<string>()));

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.UpdateQuestion("12345", "12", new UpdateQuestionDto
            {
                Content = "Enter test value"
            });

            Assert.True(response.StatusCode == 404);
        }

        [Fact]
        public async Task ShouldFailtoUpdateQuestionIfCommitFails()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>()))
                .ReturnsAsync(true);

            _questionRepositoryMock.Setup(repo => repo.GetQuestion(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Question("12", "Content", 1, "CustomInformation", "Paragraph"));

            _questionRepositoryMock.Setup(repo => repo.UpdateQuestion(It.IsAny<Question>())).ReturnsAsync(false);

            var questionService = new QuestionService(_questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await questionService.UpdateQuestion("12345", "12", new UpdateQuestionDto
            {
                Content = "Enter test value"
            });

            Assert.True(response.StatusCode == 400);
        }
    }
}

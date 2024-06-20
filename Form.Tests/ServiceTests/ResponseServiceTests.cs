using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Services;
using FormAPI.Models.Answers;
using FormAPI.Models.DTOs.ResponseDTOs;
using Moq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Form.Tests.ServiceTests
{
    public class ResponseServiceTests
    {
        [Fact]
        public async Task ShouldSubmitResponseSuccessfully()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IResponseRepository> _responseRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _questionRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _responseRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<FormResponse>()))
                .ReturnsAsync(true);

            _formRepositoryMock.Setup(s => s.IsExist(It.IsAny<string>())).ReturnsAsync(true);

            var json = JsonNode.Parse("""{"Response":"Adeola"}""");

            var responseService = new ResponseService(_responseRepositoryMock.Object,_questionRepositoryMock.Object,_formRepositoryMock.Object);

            var response = await responseService.SubmitResponse("12", [
                 new CreateResponseDto
                 {
                     QuestionId="11",
                     Response= json["Response"].Deserialize<JsonElement>()
                 }
                ]);

            Assert.True(response.StatusCode == 200);
        }

        [Fact]
        public async Task ShouldFailToSubmitResponseIfQuestionDoNotExist()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IResponseRepository> _responseRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _questionRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            _formRepositoryMock.Setup(s => s.IsExist(It.IsAny<string>())).ReturnsAsync(true);

            var json = JsonNode.Parse("""{"Response":"Adeola"}""");

            var responseService = new ResponseService(_responseRepositoryMock.Object, _questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await responseService.SubmitResponse("12", [
                 new CreateResponseDto
                 {
                     QuestionId="11",
                     Response= json["Response"].Deserialize<JsonElement>()
                 }
                ]);

            Assert.True(response.StatusCode == 404);
        }

        [Fact]
        public async Task ShouldFailToSubmitResponseIfFormDoNotExist()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IResponseRepository> _responseRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _formRepositoryMock.Setup(s => s.IsExist(It.IsAny<string>())).ReturnsAsync(false);

            var json = JsonNode.Parse("""{"Response":"Adeola"}""");

            var responseService = new ResponseService(_responseRepositoryMock.Object, _questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await responseService.SubmitResponse("12", [
                 new CreateResponseDto
                 {
                     QuestionId="11",
                     Response= json["Response"].Deserialize<JsonElement>()
                 }
                ]);

            Assert.True(response.StatusCode == 404);
        }

        [Fact]
        public async Task ShouldFailToSubmitResponseIfCommitFails()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IResponseRepository> _responseRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();
            
            _questionRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _responseRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<FormResponse>()))
                .ReturnsAsync(false);

            _formRepositoryMock.Setup(s => s.IsExist(It.IsAny<string>())).ReturnsAsync(true);

            var json = JsonNode.Parse("""{"Response":"Adeola"}""");

            var responseService = new ResponseService(_responseRepositoryMock.Object, _questionRepositoryMock.Object, _formRepositoryMock.Object);

            var response = await responseService.SubmitResponse("12", [
                 new CreateResponseDto
                 {
                     QuestionId="11",
                     Response= json["Response"].Deserialize<JsonElement>()
                 }
                ]);

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public async Task ShouldFailToSubmitResponseIfReponseDoesNotGenerate()
        {
            Mock<IQuestionRepository> _questionRepositoryMock = new();
            Mock<IResponseRepository> _responseRepositoryMock = new();
            Mock<IFormRepository> _formRepositoryMock = new();

            _questionRepositoryMock.Setup(repo => repo.IsExist(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _responseRepositoryMock.Setup(repo => repo.AddItem(It.IsAny<FormResponse>()))
                .ReturnsAsync(true);

            _formRepositoryMock.Setup(s=>s.IsExist(It.IsAny<string>())).ReturnsAsync(true);

            var json = JsonNode.Parse("""{"Response":"Adeola"}""");

            var responseService = new ResponseService(_responseRepositoryMock.Object, _questionRepositoryMock.Object,_formRepositoryMock.Object);

            var response = await responseService.SubmitResponse("12", [
                 new CreateResponseDto
                 {
                     QuestionId="11",
                     Response= json
                 }
                ]);

            Assert.True(response.StatusCode == 400);
        }
    }
}

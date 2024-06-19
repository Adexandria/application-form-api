using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Utility;
using FormAPI.Models.Answers;
using FormAPI.Models.DTOs.QuestionDTOs.CreateQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.FetchQuestions;
using FormAPI.Models.DTOs.QuestionDTOs.UpdateQuestions;
using FormAPI.Models.DTOs.ResponseDTOs;
using FormAPI.Models.Enums;
using FormAPI.Models.Questions;
using Mapster;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FormAPI.Infrastructure.Extensions
{
    /// <summary>
    /// Handles extensions for all models
    /// </summary>
    public static class ModelExtension
    {
        /// <summary>
        /// Creates question model based on it's question type
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="newQuestion">A model managing new questions</param>
        /// <returns>Question</returns>

        public static Question CreateQuestionBasedOnType(this CreateQuestionDto newQuestion, string formId)
        {
            //Creates question based on it's type
            Question question = newQuestion.QuestionType switch
            {
                string questionType when questionType == QuestionType.Multichoice.ToString() =>
                new MultiChoiceQuestion(formId, newQuestion.Content, newQuestion.QuestionNumber, newQuestion.QuestionGroup)
                                        .AddChoices(newQuestion.Choices, newQuestion.MaxChoices),

                string questionType when questionType == QuestionType.DropDown.ToString()
                => new DropDownQuestion(formId, newQuestion.Content, newQuestion.QuestionNumber, newQuestion.QuestionGroup)
                                        .AddChoices(newQuestion.Choices),

                _ => new Question(formId, newQuestion.Content, newQuestion.QuestionNumber, newQuestion.QuestionGroup, newQuestion.QuestionType),
            };
            return question;
        }


        /// <summary>
        /// Creates reponses based on question types
        /// </summary>
        /// <param name="newResponse">A model to create responses</param>
        /// <returns>Answer</returns>
        public static Answer CreateReponsesBasedOnQuestionType(this CreateResponseDto newResponse)
        {
            Answer answer = newResponse.Response switch
            {
                JsonElement value when value.ValueKind == JsonValueKind.False || value.ValueKind == JsonValueKind.True  =>
                new YesNoSubmittedAnswer(value.GetBoolean(), newResponse.QuestionId),

                JsonElement value when value.ValueKind == JsonValueKind.Array =>
                new MultiChoiceSubmittedAnswer(newResponse.QuestionId).AddChoiceIds(value.Deserialize<JsonArray>()),                

                JsonElement value when value.ValueKind == JsonValueKind.String
                => new SubmittedAnswer(value.GetString(), newResponse.QuestionId),

                JsonElement value when value.ValueKind == JsonValueKind.Number 
                => new SubmittedAnswer(value.GetInt32(), newResponse.QuestionId),

                _ => null
            };

            return answer;
        }




        /// <summary>
        /// Create question dto based on question type
        /// </summary>
        /// <param name="currentQuestion">Current question</param>
        /// <param name="questionChoice">Mnagaes the choices of a question</param>
        /// <returns>A question dto</returns>
        private static T CreateQuestionDtoBasedOnType<T>(this Question currentQuestion,QuestionChoice questionChoice) where T : QuestionDto
        {
            T newQuestionDto = Activator.CreateInstance<T>();

            newQuestionDto.Content = currentQuestion.Content;

            newQuestionDto.QuestionId = currentQuestion.Id;

            T questionDto = currentQuestion.QuestionType switch
            {
                QuestionType questionType when questionType == QuestionType.DropDown && newQuestionDto is DropDownQuestionDto value =>
                value.AddChoices(questionChoice.Choices) as T,

                QuestionType questionType when questionType == QuestionType.Multichoice && newQuestionDto is MultiChoiceQuestionDto value=>
                value.AddChoices(questionChoice.Choices, questionChoice.MaxChoices) as T,

                _ => newQuestionDto 
            };

            return questionDto;
        }

        /// <summary>
        /// Create question dtos based on question type
        /// </summary>
        /// <param name="currentQuestions">Current questions</param>
        /// <param name="questionChoices">Manages the choices of questions</param>
        /// <returns>A list of question dto</returns>
        public static List<T> CreateQuestionsDtosBasedOnType<T>(this List<Question> currentQuestions, List<QuestionChoice> questionChoices) where T : QuestionDto
        {
            List<T> questionsDto = [];
            foreach (Question currentQuestion in currentQuestions)
            {
                var questionChoice = questionChoices.FirstOrDefault(s => s.id == currentQuestion.Id);

                T question = currentQuestion.CreateQuestionDtoBasedOnType<T>(questionChoice);

                questionsDto.Add(question);
            }

            return questionsDto;
        }

        /// <summary>
        /// Updates an existing question based on it's type
        /// </summary>
        /// <param name="currentQuestion">Curent question object</param>
        /// <param name="updateQuestionDto">New question to update</param>
        /// <returns>Question</returns>
        public static Question UpdateQuestionExistingBasedOnType(this Question currentQuestion, UpdateQuestionDto updateQuestionDto)
        {

            Question question = currentQuestion.QuestionType switch
            {
                QuestionType questionType when questionType == QuestionType.DropDown =>
                currentQuestion.Adapt<DropDownQuestion>().UpdateChoices(updateQuestionDto.Choices)
                    .UpdateContent(updateQuestionDto.Content),

                QuestionType questionType when questionType == QuestionType.Multichoice =>
                currentQuestion.Adapt<MultiChoiceQuestion>()
                    .UpdateChoices(updateQuestionDto.Choices, updateQuestionDto.MaxChoices)
                    .UpdateContent(updateQuestionDto.Content),


                _ => currentQuestion.UpdateContent(updateQuestionDto.Content)
            };

            return question;

        }
    }
}

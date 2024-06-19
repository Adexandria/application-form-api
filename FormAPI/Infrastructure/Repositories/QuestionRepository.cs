using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Infrastructure.Utility;
using FormAPI.Models.Enums;
using FormAPI.Models.Questions;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace FormAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Manages question container
    /// </summary>
    /// <param name="database"></param>
    public class QuestionRepository(Database database) : IQuestionRepository
    {

        /// <summary>
        /// Adds new question to the container
        /// </summary>
        /// <param name="item">Manages question model for all question types</param>
        /// <returns>A boolean value to denote if it was successfully</returns>
        public async Task<bool> AddItem(Question item)
        {
           var response = await _container.CreateItemAsync(item, new PartitionKey(item.FormId));
           
            if(response.StatusCode != HttpStatusCode.Created)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Updates an existing question in a container
        /// </summary>
        /// <param name="item">Manages question model for all question types</param>
        /// <returns>A boolean value to denote if it was successfully</returns>
        public async Task<bool> UpdateQuestion(Question item)
        {
            var response = await _container.ReplaceItemAsync(item, item.Id, new PartitionKey(item.FormId));

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Fetches a list of question using question type
        /// </summary>
        /// <param name="questionType">Question type</param>
        /// <param name="formId">Form id</param>
        /// <returns>A list of questions</returns>
        public async Task<List<Question>> GetQuestions(string formId, QuestionType questionType)
        {
            List<Question> questions = [];

            var query = new QueryDefinition(
                    query: "SELECT * FROM questions p WHERE p.QuestionType = @questionType AND p.FormId = @formId ORDER BY p.QuestionNumber")
                    .WithParameter("@questionType",(int) questionType)
                    .WithParameter("@formId", formId);

            var feed = _container.GetItemQueryIterator<Question>(query);

            while (feed.HasMoreResults)
            {
                FeedResponse<Question> response = await feed.ReadNextAsync();
                foreach (Question item in response)
                {
                    questions.Add(item);
                }
            }

            return questions;
        }

        /// <summary>
        /// Fetches choice in a question
        /// </summary>
        /// <param name="formId">Form id</param>
        /// <param name="questionType">Question type</param>
        /// <returns>A list of choices</returns>
        public async Task<List<QuestionChoice>> GetQuestionChoices(string formId, QuestionType questionType)
        {
            List<QuestionChoice> choices = [];
            var query = new QueryDefinition(
                    query: "SELECT p.Choices, p.MaxChoices, p.id FROM questions p WHERE p.FormId = @formId AND p.QuestionType = @questionType")
                    .WithParameter("@formId", formId)
                    .WithParameter("@questionType", (int)questionType);

            var feed = _container.GetItemQueryIterator<QuestionChoice>(query);

            while (feed.HasMoreResults)
            {
                FeedResponse<QuestionChoice> response = await feed.ReadNextAsync();
                foreach (QuestionChoice item in response)
                {
                    choices.Add(item);
                }
            }

            return choices;
        }


        /// <summary>
        /// Fetches a single question using the id and partition key
        /// </summary>
        /// <param name="id">Question id</param>
        /// <param name="partitionKey">Partition key</param>
        /// <returns>A question</returns>
        public async Task<Question> GetQuestion(string id, string partitionKey)
        {
            var response = await _container.ReadItemAsync<Question>(id, new PartitionKey(partitionKey));

            return response.Resource;
        }

        /// <summary>
        /// Verifies if question exists
        /// </summary>
        /// <param name="id">Question id</param>
        /// <param name="partitionKey">Partition key</param>
        /// <returns>boolean value</returns>
        public async Task<bool> IsExist(string id, string partitionKey)
        {
            var response = await _container.ReadItemAsync<Question>(id, new PartitionKey(partitionKey));

            return response.Resource != null;
        }




        /// <summary>
        /// A container that manages questions
        /// </summary>
        private readonly Container _container = database.CreateContainerIfNotExistsAsync("questions", "/FormId").Result;
    }
}

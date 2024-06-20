namespace FormAPI.Models.DTOs
{
    /// <summary>
    /// Manages how choice will be displayed
    /// </summary>
    public class ChoiceDto
    {
        /// <summary>
        /// Choice id
        /// </summary>

        public string Id {  get; set; }
        /// <summary>
        /// Content of the choice
        /// </summary>
        public string Content { get; set; }
    }
}

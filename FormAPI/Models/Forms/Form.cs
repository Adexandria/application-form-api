namespace FormAPI.Models.Forms
{
    /// <summary>
    /// A model to manage form
    /// </summary>
    /// <param name="title">Title of form</param>
    /// <param name="description">Description of form</param>
    public class Form(string title, string description) : BaseClass()
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = title;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = description;
    }
}

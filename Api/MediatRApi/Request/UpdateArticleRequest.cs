namespace MediatRApi.Request
{
    public class UpdateArticleRequest : CreateArticleRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = null!;
    }
}

using MediatR;

namespace MediatRApi.Request
{
    public class CreateArticleRequest : IRequest<string>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// 摘要
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// 内容
        /// </summary>Ï
        public string Content { get; set; } = null!;

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; } = null!;

        /// <summary>
        /// 来源
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// 来源地址
        /// </summary>
        public string? SourceUrl { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string? Cover { get; set; }
    }
}

using MediatR;

namespace MediatRApi.Request
{
    public class DeleteArticleRequest:IRequest<string>
    {
        public string Id { get; set; }
    }
}

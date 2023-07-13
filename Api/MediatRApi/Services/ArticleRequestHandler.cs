using MediatRApi.Request;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace MediatRApi.Services
{
    public class ArticleRequestHandler: 
        IRequestHandler<CreateArticleRequest, string>,
        IRequestHandler<UpdateArticleRequest, string>,
        IRequestHandler<DeleteArticleRequest, string>
    {

        public async Task<string> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
        {
            //todo 数据库操作
            return "Add";
        }

        public async Task<string> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            //todo 数据库操作
            return "Update";
        }

        public async Task<string> Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
        {
            //todo 数据库操作
            return "Delete";
        }
    }
}

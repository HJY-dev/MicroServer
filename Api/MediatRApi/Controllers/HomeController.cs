using MediatR;
using MediatRApi.Event;
using MediatRApi.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<string> PostAsync([FromBody] CreateArticleRequest request,
        CancellationToken cancellationToken)
        {
            return _mediator.Send(request, cancellationToken);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<string> PutAsync([FromBody] UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            return _mediator.Send(request, cancellationToken);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        public Task<string> DeleteAsync([FromBody] DeleteArticleRequest request, CancellationToken cancellationToken)
        {
            return _mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// 添加用户
        /// 发送邮件-领域事件
        /// </summary>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> AddUser([FromServices] IMediator mediator)
        {
            // 模拟注册成功
            bool isSuccess = true;

            if(isSuccess)
            {
                //发布注册成功事件,邮件、短信通知
                await mediator.Publish(new AddUserSuccessEvent { 
                    UserId = Guid.NewGuid().ToString(),
                    UserName = "Van"
                });
            }
            return "添加成功";
        }
    }
}

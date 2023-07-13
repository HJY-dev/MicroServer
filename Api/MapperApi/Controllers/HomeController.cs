using AutoMapper;
using MapperApi.Model.Entity;
using MapperApi.Model.RequestDto;
using MapperApi.Model.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapperApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly static List<ActivityEntities> activityList = new List<ActivityEntities>(){ new ActivityEntities {
                KeyId = Guid.NewGuid(),
                ActiveName = $"测试1",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
            },new ActivityEntities {
                KeyId = Guid.NewGuid(),
                ActiveName = $"测试2",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
            },new ActivityEntities {
                KeyId = Guid.NewGuid(),
                ActiveName = $"测试3",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
            },new ActivityEntities {
                KeyId = Guid.NewGuid(),
                ActiveName = $"测试4",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
            }};

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取全部活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ActivityResponseDto> GetAllActivity()
        {
            var data = activityList;
            var result = _mapper.Map<List<ActivityResponseDto>>(data);
            return result;
        }

        /// <summary>
        /// 获取活动明细
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActivityResponseDto GetDetailByName(string activityName)
        {
            var data = activityList.Where(x=>x.ActiveName == activityName).FirstOrDefault();
            var result = _mapper.Map<ActivityResponseDto>(data);
            return result;
        }

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddActivity(CreateActivityRequestDto req)
        {
            var entity = _mapper.Map<ActivityEntities>(req);
            entity.KeyId = Guid.NewGuid();
            activityList.Add(entity);
            return true;
        }

    }
}

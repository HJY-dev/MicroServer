using MicroServer.CheckWords.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServer.CheckWords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinganController : ControllerBase
    {
        [HttpGet]
        [Route("Check")]
        public string Check([FromQuery] MinganCheckInput input)
        {
            return input.Text;
        }

        [HttpGet]
        [Route("Replace")]
        public string Replace([FromQuery] MinganReplaceInput input)
        {
            return $"替换后的字段为:{input.Text}";
        }
    }
}

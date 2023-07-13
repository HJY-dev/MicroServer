using MicroServer.OrganizationApi.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MicroServer.OrganizationApi.Extensions.CustomApiVersion;

namespace MicroServer.OrganizationApi.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApbController : ControllerBase
    {
        [HttpGet]
        [CustomRoute(ApiVersions.V1, "apbs")]
        public IEnumerable<string> Get()
        {
            return new string[] { "第一版的 apbs" };
        }
    }
}

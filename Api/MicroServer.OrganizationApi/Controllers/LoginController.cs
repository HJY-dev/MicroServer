using MicroServer.Common.GlobalVar;
using MicroServer.Common.Helper;
using MicroServer.Common.Model;
using MicroServer.Common.ServiceExtensions.Policys;
using MicroServer.Organization.Entities;
using MicroServer.Organization.Services;
using MicroServer.OrganizationApi.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MicroServer.OrganizationApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiCpntroller
    {
        private readonly IFreeSql<MallContext> _freeSql;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;

        public LoginController(IFreeSql<MallContext> freeSql, IRoleModulePermissionServices roleModulePermissionServices)
        {
            _freeSql = freeSql;
            _roleModulePermissionServices = roleModulePermissionServices;
        }

        /// <summary>
        /// 获取JWT的方法3：整个系统主要方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("JWTToken3.0")]
        //public async Task<MessageModel<TokenInfoViewModel>> GetJwtToken3(string name = "", string pass = "")
        //{
        //    string jwtStr = string.Empty;

        //    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
        //        return Failed<TokenInfoViewModel>("用户名或密码不能为空");

        //    pass = MD5Helper.MD5Encrypt32(pass);

        //    var user = await _sysUserInfoServices.Query(d => d.uLoginName == name && d.uLoginPWD == pass && d.tdIsDelete == false);
        //    if (user.Count > 0)
        //    {
        //        var userRoles = await _sysUserInfoServices.GetUserRoleNameStr(name, pass);
        //        //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
        //        var claims = new List<Claim> {
        //            new Claim(ClaimTypes.Name, name),
        //            new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().uID.ToString()),
        //            new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
        //        claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));


        //        // ids4和jwt切换
        //        // jwt
        //        if (!Permissions.IsUseIds4)
        //        {
        //            var data = await _roleModulePermissionServices.RoleModuleMaps();
        //            var list = (from item in data
        //                        where item.IsDeleted == false
        //                        orderby item.Id
        //                        select new PermissionItem
        //                        {
        //                            Url = item.Module?.LinkUrl,
        //                            Role = item.Role?.Name.ObjToString(),
        //                        }).ToList();

        //            _requirement.Permissions = list;
        //        }

        //        var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
        //        return Success(token, "获取成功");
        //    }
        //    else
        //    {
        //        return Failed<TokenInfoViewModel>("认证失败");
        //    }
        //}

        /// <summary>
        /// 请求刷新Token（以旧换新）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("RefreshToken")]
        //public async Task<MessageModel<TokenInfoViewModel>> RefreshToken(string token = "")
        //{
        //    string jwtStr = string.Empty;

        //    if (string.IsNullOrEmpty(token))
        //        return Failed<TokenInfoViewModel>("token无效，请重新登录！");
        //    var tokenModel = JwtHelper.SerializeJwt(token);
        //    if (tokenModel != null && tokenModel.Uid > 0)
        //    {
        //        var user = await _sysUserInfoServices.QueryById(tokenModel.Uid);
        //        if (user != null)
        //        {
        //            var userRoles = await _sysUserInfoServices.GetUserRoleNameStr(user.uLoginName, user.uLoginPWD);
        //            //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
        //            var claims = new List<Claim> {
        //            new Claim(ClaimTypes.Name, user.uLoginName),
        //            new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ObjToString()),
        //            new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
        //            claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

        //            //用户标识
        //            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        //            identity.AddClaims(claims);

        //            var refreshToken = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
        //            return Success(refreshToken, "获取成功");
        //        }
        //    }
        //    return Failed<TokenInfoViewModel>("认证失败！");
        //}

        
        [HttpGet]
        [Route("TestAAA")]
        public async Task<MessageModel<TokenInfoViewModel>> TestAAA()
        {
            var data = await _roleModulePermissionServices.RoleModuleMaps();
            return Failed<TokenInfoViewModel>("认证失败！");
        }

        /// <summary>
        /// 测试 MD5 加密字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Md5Password")]
        public string Md5Password(string password = "")
        {
            return MD5Helper.MD5Encrypt32(password);
        }
    }
}

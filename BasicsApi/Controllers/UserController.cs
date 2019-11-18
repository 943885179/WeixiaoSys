using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private WeixiaoSysContext _db;
        public UserController(WeixiaoSysContext db, IMapper mapper, IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            _db = db;
            _mapper = mapper;
            _jwtSettings = _jwtSettingsAccesser.Value;
        }
        [HttpPost("Login")]
        public ResponseDto Login(Employee user){
            var result = new ResponseDto();
            var users =  _db.Employee.Where(o => o.LoginName == user.LoginName && o.LoginPwd == user.LoginPwd).FirstOrDefault();
            if (users==null ||(user.LoginName!="admin" || user.LoginPwd!="123"))
            {
                var claim = new Claim[]{ //声明
                    new Claim(ClaimTypes.Name,"weixiaoqaq"),
                    new Claim(ClaimTypes.Role,"admin")
                };
                //对称秘钥
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                //签名证书(秘钥，加密算法)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);

                result.data= new { token = new JwtSecurityTokenHandler().WriteToken(token) };
            }
            return result;
        }
    }
}

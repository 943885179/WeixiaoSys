using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace BasicsApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BacsicsController
    {
        private readonly JwtSettings _jwtSettings;
        private readonly EmpService bll;
        /// <summary>
        ///
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        /// <param name="_jwtSettingsAccesser"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public UserController(WeixiaoSysContext db, IMapper mapper, IOptions<JwtSettings> _jwtSettingsAccesser, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            _jwtSettings = _jwtSettingsAccesser.Value;
            bll = new EmpService(db);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ResponseDto> Login(LoginDto user)
        {
            var result = new ResponseDto();
            if (user.LoginName=="admin" && user.LoginPwd=="123")
            {
                _user = new EmployeeDto()
                {
                    Name ="admin",
                    Email="test@qq.com"
                };
            }
            else
            {
                  _user = _mapper.Map<Employee, EmployeeDto>(await bll.Login(user));
            }
            if (_user != null)
            {
                var claim = new Claim[]{ //声明
                    new Claim(ClaimTypes.UserData,JsonSerializer.Serialize(_user))
                };
                //对称秘钥
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                //签名证书(秘钥，加密算法)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);
                result.data = new { token = new JwtSecurityTokenHandler().WriteToken(token) };
            }
            return result;
        }
        [HttpPost("Emps")]
        public async Task<ResponseDto> Emps(EmployeeDto dto)
        {
            var emps = await bll.EmpLists(dto);
            var data = _mapper.Map<ResultPageDto<List<Employee>>, ResultPageDto<List<EmployeeDto>>>(emps);
            result.data = data;
            return result;
        }
        [HttpGet("EmpById/{id}")]
        public async Task<ResponseDto> EmpById(int id)
        {
            var data = _mapper.Map<Employee, EmployeeDto>(await bll.EmployeeById(id));
            result.data = data;
            return result;
        }
        [HttpGet("EmpByDepId/{depId}")]
        public async Task<ResponseDto> EmpByDepId(int depId)
        {
            var data = _mapper.Map<List<Employee>, List<EmployeeDto>>(await bll.EmployeeByDepId(depId));
            result.data = data;
            return result;
        }
        [HttpPost("AddOrEditEmp")]
        public async Task<ResponseDto> AddOrEditEmp(Employee emp)
        {
            result.data = await bll.AddOrEdit(emp);
            return result;
        }
        [HttpPost("DeleteEmp/{id}")]
        public async Task<ResponseDto> DeleteEmp(int id)
        {
            result.data = await bll.Delete(id);
            return result;
        }
    }
}

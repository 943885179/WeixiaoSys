using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private WeixiaoSysContext _db;
        public UserController(WeixiaoSysContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public ResponseDto Login(Employee user){
            var result = new ResponseDto();
            var users =  _db.Employee.Where(o => o.LoginName == user.LoginName && o.LoginPwd == user.LoginPwd).FirstOrDefault();
            if (users==null ||(user.LoginName!="admin" || user.LoginPwd!="123"))
            {
                result.data =new Employee() { 
                    LoginName="admin",
                    Name="weixiao",
                    Img= "https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png"
                };
            }
            return  result;
        }
    }
}
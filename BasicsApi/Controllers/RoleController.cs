using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.conmon;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BacsicsController
    {
        private readonly RoleService bll;
        public RoleController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            bll = new RoleService(db);
        }
        [HttpGet("Role")]
        public async Task<ActionResult<ResponseDto>> Role()
        {
            result.data = _mapper.Map<List<RoleDto>>(await bll.Roles());
            return result;
        }
        [HttpGet("SelectRole")]
        public async Task<ActionResult<ResponseDto>> SelectRole()
        {
            var data = await bll.SelectRoles();
            result.data = data;
            return result;
        }
        [HttpPost("Roles")]
        public async Task<ActionResult<ResponseDto>> Roles(RoleDto dto)
        {
            result.data = _mapper.Map<ResultPageDto<List<Role>>, ResultPageDto<List<RoleDto>>>(await bll.RoleList(dto));
            return result;
        }
        [HttpGet("RoleById/{id}")]
        public async Task<ActionResult<ResponseDto>> RoleById(int id)
        {
            var Role = await bll.RoleById(id);
            result.data = _mapper.Map<RoleDto>(Role);
            return result;
        }
        [HttpPost("AddOrEditRole")]
        public async Task<ActionResult<ResponseDto>> AddOrEditRole(RoleDto dto)
        {
            result.data = await bll.AddOrEditAsync(dto);
            return result;
        }
        [HttpPost("DeleteRole/{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteRole(int id)
        {
            result.data = await bll.Delete(id);
            return result;
        }
    }
}

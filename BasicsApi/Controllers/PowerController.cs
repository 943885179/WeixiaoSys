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

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerController : BacsicsController
    {
        private readonly PowerService bll;
        public PowerController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            bll = new PowerService(db);
        }
        [HttpGet("Power")]
        public async Task<ActionResult<ResponseDto>> Power()
        {
            result.data = _mapper.Map<List<PowerDto>>(await bll.Powers());
            return result;
        }
        [HttpGet("SelectPower")]
        public async Task<ActionResult<ResponseDto>> SelectPower()
        {
            var data = await bll.SelectPowers();
            result.data = data;
            return result;
        }
        [HttpPost("Powers")]
        public async Task<ActionResult<ResponseDto>> Powers(PowerDto dto)
        {
            var data = _mapper.Map<ResultPageDto<List<Power>>, ResultPageDto<List<PowerDto>>>(await bll.PowerList(dto));
            result.data = data;
            return result;
        }
        [HttpGet("PowerById/{id}")]
        public async Task<ActionResult<ResponseDto>> PowerById(int id)
        {
            var Power = await bll.PowerById(id);
            result.data = _mapper.Map<PowerDto>(Power);
            return result;
        }
        [HttpPost("AddOrEditPower")]
        public async Task<ActionResult<ResponseDto>> AddOrEditPower(PowerDto dto)
        {
            result.data = await bll.AddOrEditAsync(dto);
            return result;
        }
        [HttpPost("DeletePower/{id}")]
        public async Task<ActionResult<ResponseDto>> DeletePower(int id)
        {
            result.data = await bll.Delete(id);
            return result;
        }

    }
}

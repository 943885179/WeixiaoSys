using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.conmon;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : BacsicsController
    {
        private readonly AreaService bll;
        public AreaController(WeixiaoSysContext db, IMapper mapper,IOptions<RSASettings> setting):base(db,mapper,setting)
        {
            bll = new AreaService(db);
        }
        [HttpGet("Area")]
        public async Task<RsaResponseDto> Area()
        {
            result.data = _mapper.Map<List<AreaDto>>(await bll.Areas(null));
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpGet("SelectArea")]
        public async Task<RsaResponseDto> SelectArea()
        {
            result = new ResponseDto();
            result.data = await bll.SelectAreas(null);
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpGet("GetAreaByIds/{ids}")]
        public async Task<RsaResponseDto> GetAreaByIds(string ids)
        {
            result = new ResponseDto();
            result.data = await bll.GetAreaByIds(ids);
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
    }
}

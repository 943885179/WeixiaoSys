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
    /// <summary>
    /// 地区基础信息管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : BacsicsController
    {
        private readonly AreaService bll;
        public AreaController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            bll = new AreaService(db);
        }
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("Area.json")]
        public async Task<ResponseDto> Area()
        {
            result.data = _mapper.Map<List<AreaDto>>(await bll.Areas(null));
            return result;
        }
        /// <summary>
        /// 获取地区下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet("SelectArea.json")]
        public async Task<ResponseDto> SelectArea()
        {
            result.data = await bll.SelectAreas(null);
            return result;
        }
        /// <summary>
        /// 通过地区表Id获取地区
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("GetAreaByIds/{ids}")]
        public async Task<ResponseDto> GetAreaByIds(string ids)
        {
            result.data = await bll.GetAreaByIds(ids);
            return result;
        }
    }
}

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

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : BacsicsController
    {
        private readonly AreaService bll;
        public AreaController(WeixiaoSysContext db, IMapper mapper) : base(db, mapper)
        {
            bll = new AreaService(db);
        }
        [HttpGet("Area")]
        public async Task<ActionResult<ResponseDto>> Area()
        {
            try
            {
                result.data = _mapper.Map<List<AreaDto>>(await bll.Areas(null));
            }
            catch (WeixiaoException ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            catch (Exception ex)
            {
                throw ex;
                //return NotFound();//404
                //return BadRequest();//400
                //return Ok(result.msg);
                // return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError) { };
            }
            return result;
        }
        [HttpGet("SelectArea")]
        public async Task<ActionResult<ResponseDto>> SelectArea()
        {
            result = new ResponseDto();
            try
            {
                result.data = await bll.SelectAreas(null);
            }
            catch (WeixiaoException ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        [HttpGet("GetAreaByIds/{ids}")]
        public async Task<ActionResult<ResponseDto>> GetAreaByIds(string ids)
        {
            result = new ResponseDto();
            try
            {
                result.data = await bll.GetAreaByIds(ids);
            }
            catch (WeixiaoException ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}

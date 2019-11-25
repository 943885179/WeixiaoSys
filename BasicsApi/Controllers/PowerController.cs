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
        private readonly CompanyService bll ;
        public PowerController(WeixiaoSysContext db, IMapper mapper,IOptions<RSASettings> setting):base(db,mapper,setting)
        {
            bll = new CompanyService(db);
        }
        [HttpGet("Company")]
        public async Task<ActionResult<ResponseDto>> Company()
        {
            try
            {
                result.data = _mapper.Map<List<CompanyDto>>(await bll.Companys(null));
            }
            catch(WeixiaoException ex)
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
        [HttpGet("SelectCompany")]
        public async Task<ActionResult<ResponseDto>> SelectCompany()
        {
            result = new ResponseDto();
            try
            {
                result.data =await bll.SelectCompanys(null);
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
        [HttpPost("Companys")]
        public async Task<ActionResult<ResponseDto>> Companys(CompanyDto dto)
        {
            try
            {
                result.data = _mapper.Map<ResultPageDto<List<Company>>,ResultPageDto<List<CompanyDto>>>(await bll.CompanyList(dto));
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
        [HttpGet("ShareholderByCid/{cid}")]
        public async Task<ActionResult<ResponseDto>> ShareholderByCid(int cid)
        {
            try
            {
                result.data =await bll.ShareholderByCid(cid);
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
        [HttpGet("CompanyLogByCid/{cid}")]
        public async Task<ActionResult<ResponseDto>> CompanyLogByCid(int cid)
        {
            try
            {
                result.data = await bll.CompanyLogByCid(cid);
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
        [HttpGet("CompanyById/{id}")]
        public async Task<ActionResult<ResponseDto>> CompanyById(int id)
        {
            var Company=await bll.CompanyById(id);
            result.data = _mapper.Map<CompanyDto>(Company);
            return result;
        }
        [HttpPost("AddOrEditCompany/{isShare=0}")]
        public async Task<ActionResult<ResponseDto>> AddOrEditCompany(Company company,int isShare)
        {
            try
            {
                if (company.Id > 0)
                {
                    result.data = await bll.Edit(company,isShare);
                }
                else
                {
                    result.data = await bll.Add(company);
                }
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
        [HttpPost("DeleteCompany/{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteCompany(int id)
        {
            try
            {
                result.data = await bll.Delete(id);
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
       [HttpPost("DeleteCompanys")]
        public async Task<ActionResult<ResponseDto>> DeleteCompanys(List<EntityDto> ids)
        {
            try
            {
                result.data = await bll.Deletes(ids);
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

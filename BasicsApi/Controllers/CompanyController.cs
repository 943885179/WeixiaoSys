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
using Newtonsoft.Json;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BacsicsController
    {
        private readonly CompanyService bll ;
        public CompanyController(WeixiaoSysContext db, IMapper mapper,IOptions<RSASettings> setting):base(db,mapper,setting)
        {
            bll = new CompanyService(db);
        }
        [HttpGet("Company")]
        public async Task<RsaResponseDto> Company()
        {
             result.data = _mapper.Map<List<CompanyDto>>(await bll.Companys(null));
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpGet("SelectCompany")]
        public async Task<RsaResponseDto> SelectCompany()
        {
            result.data =await bll.SelectCompanys(null);
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpPost("Companys")]
        public async Task<RsaResponseDto> Companys(CompanyDto dto)
        {

             result.data = _mapper.Map<ResultPageDto<List<Company>>,ResultPageDto<List<CompanyDto>>>(await bll.CompanyList(dto));
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpGet("ShareholderByCid/{cid}")]
        public async Task<RsaResponseDto> ShareholderByCid(int cid)
        {
            result.data =await bll.ShareholderByCid(cid);
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpGet("CompanyLogByCid/{cid}")]
        public async Task<RsaResponseDto> CompanyLogByCid(int cid)
        {
            result.data = await bll.CompanyLogByCid(cid);
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpGet("CompanyById/{id}")]
        public async Task<RsaResponseDto> CompanyById(int id)
        {
            var Company=await bll.CompanyById(id);
            result.data = _mapper.Map<CompanyDto>(Company);
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpPost("AddOrEditCompany/{isShare=0}")]
        public async Task<RsaResponseDto> AddOrEditCompany(Company company,int isShare)
        {

                if (company.Id > 0)
                {
                    result.data = await bll.Edit(company,isShare);
                }
                else
                {
                    result.data = await bll.Add(company);
                }
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
        [HttpPost("DeleteCompany/{id}")]
        public async Task<RsaResponseDto> DeleteCompany(int id)
        {
            result.data = await bll.Delete(id);
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
       [HttpPost("DeleteCompanys")]
        public async Task<RsaResponseDto> DeleteCompanys(List<EntityDto> ids)
        {

            result.data = await bll.Deletes(ids);
             res.Data= rsa.Encrypt(JsonConvert.SerializeObject(result));
            return res;
        }
    }
}

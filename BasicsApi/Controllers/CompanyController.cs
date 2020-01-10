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
        private readonly CompanyService bll;
        public CompanyController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            bll = new CompanyService(db);
        }
        [HttpGet("xx")]
        public bool Test(){
           return  bll.Test();
        }









        [HttpGet("Company")]
        public async Task<ResponseDto> Company()
        {
            result.data = _mapper.Map<List<CompanyDto>>(await bll.Companys(null));
            return result;
        }
        [HttpGet("SelectCompany")]
        public async Task<ResponseDto> SelectCompany()
        {
            var data= await bll.SelectCompanys(null);
            result.data = data;
            return result;
        }
        [HttpPost("Companys")]
        public async Task<ResponseDto> Companys(CompanyDto dto)
        {
            result.data = _mapper.Map<ResultPageDto<List<Company>>, ResultPageDto<List<CompanyDto>>>(await bll.CompanyList(dto));
            return result;
        }
        [HttpGet("ShareholderByCid/{cid}")]
        public async Task<ResponseDto> ShareholderByCid(int cid)
        {
            result.data = await bll.ShareholderByCid(cid);
            return result;
        }
        [HttpGet("CompanyLogByCid/{cid}")]
        public async Task<ResponseDto> CompanyLogByCid(int cid)
        {
            result.data = await bll.CompanyLogByCid(cid);
            return result;
        }
        [HttpGet("CompanyById/{id}")]
        public async Task<ResponseDto> CompanyById(int id)
        {
            var Company = await bll.CompanyById(id);
            result.data = _mapper.Map<CompanyDto>(Company);
            return result;
        }
        [HttpPost("AddOrEditCompany/{isShare=0}")]
        public async Task<ResponseDto> AddOrEditCompany(Company company, int isShare)
        {

            if (company.Id > 0)
            {
                result.data = await bll.Edit(company, isShare);
            }
            else
            {
                result.data = await bll.Add(company);
            }
            return result;
        }
        [HttpPost("DeleteCompany/{id}")]
        public async Task<ResponseDto> DeleteCompany(int id)
        {
            result.data = await bll.Delete(id);
            return result;
        }
        [HttpPost("DeleteCompanys")]
        public async Task<ResponseDto> DeleteCompanys(List<EntityDto> ids)
        {

            result.data = await bll.Deletes(ids);
            return result;
        }
    }
}

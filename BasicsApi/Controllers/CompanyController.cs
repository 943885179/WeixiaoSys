﻿using System;
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
    public class CompanyController : BacsicsController
    {
        private readonly CompanyService bll ;
        public CompanyController(WeixiaoSysContext db, IMapper mapper):base(db,mapper)
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
        [HttpGet("CompanyById/{id}")]
        public async Task<ActionResult<ResponseDto>> CompanyById(int id)
        {
            var Company=await bll.CompanyById(id);
            result.data = _mapper.Map<CompanyDto>(Company);
            return result;
        }
        [HttpPost("AddOrEditCompany")]
        public async Task<ActionResult<ResponseDto>> AddOrEditCompany(Company Company)
        {
            try
            {
                if (Company.Id > 0)
                {
                    result.data = await bll.Edit(Company);
                }
                else
                {
                    result.data = await bll.Add(Company);
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
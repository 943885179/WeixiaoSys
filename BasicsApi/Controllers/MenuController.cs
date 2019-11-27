using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.conmon;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BacsicsController
    {
        private readonly MenuService bll ;
        public MenuController(WeixiaoSysContext db, IMapper mapper,IOptions<RSASettings> setting):base(db,mapper,setting)
        {
            bll = new MenuService(db);
        }
        [HttpGet("Menu")]
        public async Task<RsaResponseDto> Menu()
        {
            result.data = _mapper.Map<List<MenuDto>>(await bll.Menus(null));
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpGet("SelectMenu")]
        public async Task<RsaResponseDto> SelectMenu()
        {
            result.data =await bll.SelectMenus(null);
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpPost("Menus")]
        public async Task<RsaResponseDto> Menus(MenuDto dto)
        {
            result.data = _mapper.Map<ResultPageDto<List<Menu>>,ResultPageDto<List<MenuDto>>>(await bll.MenuList(dto));
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpGet("MenuById/{id}")]
        public async Task<RsaResponseDto> MenuById(int id)
        {
            var menu=await bll.MenuById(id);
            result.data = _mapper.Map<MenuDto>(menu);
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpPost("AddOrEditMenu")]
        public async Task<RsaResponseDto> AddOrEditMenu(Menu menu)
        {
                if (menu.Id > 0)
                {
                    result.data = await bll.Edit(menu);
                }
                else
                {
                    result.data = await bll.Add(menu);
                }

            res.Data= rsa.AppEncrypt(result);
            return res;
        }
        [HttpPost("DeleteMenu/{id}")]
        public async Task<RsaResponseDto> DeleteMenu(int id)
        {
            result.data = await bll.Delete(id);
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
       [HttpPost("DeleteMenus")]
        public async Task<RsaResponseDto> DeleteMenus(List<EntityDto> ids)
        {
                result.data = await bll.Deletes(ids);
            res.Data= rsa.AppEncrypt(result);
            return res;
        }
    }
}

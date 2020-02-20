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
using Microsoft.AspNetCore.Authorization;
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
        private readonly MenuService bll;
        public MenuController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            bll = new MenuService(db);
        }
        [HttpGet("Menu")]
        [AllowAnonymous]
        public async Task<ResponseDto> Menu()
        {
            result.data = _mapper.Map<List<MenuDto>>(await bll.Menus(null));
            return result;
        }
        [HttpGet("SelectMenu")]
        public async Task<ResponseDto> SelectMenu()
        {
            var data = await bll.SelectMenus(null);
            result.data = data;
            return result;
        }
        [HttpPost("Menus")]
        [WeixiaoAsyncAuthorizationFilter]
        public async Task<ResponseDto> Menus(MenuDto dto)
        {
            var x = _user;
            result.data = _mapper.Map<ResultPageDto<List<Menu>>, ResultPageDto<List<MenuDto>>>(await bll.MenuList(dto));
            return result;
        }
        [HttpGet("MenuById/{id}")]
        public async Task<ResponseDto> MenuById(int id)
        {
            var menu = await bll.MenuById(id);
            result.data = _mapper.Map<MenuDto>(menu);
            return result;
        }
        [HttpPost("AddOrEditMenu")]
        public async Task<ResponseDto> AddOrEditMenu(Menu menu)
        {
            if (menu.Id > 0)
            {
                result.data = await bll.Edit(menu);
            }
            else
            {
                result.data = await bll.Add(menu);
            }
            return result;
        }
        [HttpPost("DeleteMenu/{id}")]
        public async Task<ResponseDto> DeleteMenu(int id)
        {
            result.data = await bll.Delete(id);
            return result;
        }
        [HttpPost("DeleteMenus")]
        public async Task<ResponseDto> DeleteMenus(List<EntityDto> ids)
        {
            result.data = await bll.Deletes(ids);
            return result;
        }
    }
}

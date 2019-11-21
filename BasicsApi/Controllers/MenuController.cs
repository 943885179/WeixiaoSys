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
using Newtonsoft.Json;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BacsicsController
    {
        private readonly MenuService bll ;
        public MenuController(WeixiaoSysContext db, IMapper mapper):base(db,mapper)
        {
            bll = new MenuService(db);
        }
        [HttpGet("Menu")]
        public async Task<ActionResult<ResponseDto>> Menu()
        {
                result.data = _mapper.Map<List<MenuDto>>(await bll.Menus(null));

            return result;
        }
        [HttpGet("SelectMenu")]
        public async Task<ActionResult<ResponseDto>> SelectMenu()
        {
                result.data =await bll.SelectMenus(null);

            return result;
        }
        [HttpPost("Menus")]
        public async Task<ActionResult<ResponseDto>> Menus(MenuDto dto)
        {

                result.data = _mapper.Map<ResultPageDto<List<Menu>>,ResultPageDto<List<MenuDto>>>(await bll.MenuList(dto));

            return result;
        }
        [HttpGet("MenuById/{id}")]
        public async Task<ActionResult<ResponseDto>> MenuById(int id)
        {
            var menu=await bll.MenuById(id);
            result.data = _mapper.Map<MenuDto>(menu);
            return result;
        }
        [HttpPost("AddOrEditMenu")]
        public async Task<ActionResult<ResponseDto>> AddOrEditMenu(Menu menu)
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
        public async Task<ActionResult<ResponseDto>> DeleteMenu(int id)
        {
                result.data = await bll.Delete(id);
            return result;
        }
       [HttpPost("DeleteMenus")]
        public async Task<ActionResult<ResponseDto>> DeleteMenus(List<EntityDto> ids)
        {
                result.data = await bll.Deletes(ids);
            return result;
        }
    }
}

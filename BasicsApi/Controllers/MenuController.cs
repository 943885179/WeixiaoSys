using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class MenuController : ControllerBase
    {

        private readonly IMapper _mapper;
        private  WeixiaoSysContext _db;
        private static MenuService bll;
        private static ResponseDto result;
        public MenuController(WeixiaoSysContext db, IMapper mapper)
        {

            _db = db;
            bll = new MenuService(_db);
            _mapper = mapper;
        }
        [HttpGet("Menu")]
        public async Task<ActionResult<ResponseDto>> Menu()
        {
            result = new ResponseDto();
            try
            {
                result.data = _mapper.Map<List<MenuDto>>(await bll.Menus(null));
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            return result;
        }
        [HttpGet("SelectMenu")]
        public async Task<ActionResult<ResponseDto>> SelectMenu()
        {
            result = new ResponseDto();
            try
            {
                result.data =await bll.SelectMenus(null);
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            return result;
        }
        [HttpGet("Menus")]
        public async Task<ActionResult<ResponseDto>> Menus()
        {
            result = new ResponseDto();
            try
            {
                result.data = _mapper.Map<List<Menu>, List<MenuDto>>(await bll.MenuList());
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            return result;
        }
        [HttpGet("MenuById/{id}")]
        public async Task<ActionResult<ResponseDto>> MenuById(int id)
        {
            result = new ResponseDto();
            var menu=await bll.MenuById(id);
            result.data = _mapper.Map<MenuDto>(menu);
            return result;
        }
        [HttpPost("AddOrEditMenu")]
        public async Task<ActionResult<ResponseDto>> AddOrEditMenu(Menu menu)
        {
            result = new ResponseDto();
            try
            {
                if (menu.Id > 0)
                {
                    result.data = await bll.Edit(menu);
                }
                else
                {
                    result.data = await bll.Add(menu);
                }
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message.ToString();
            }
            return result;
        }
        [HttpPost("DeleteMenu/{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteMenu(int id)
        {
            result = new ResponseDto();
            try
            {
                result.data = await bll.Delete(id);
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message.ToString();
            }
            return result;
        }
    }
}

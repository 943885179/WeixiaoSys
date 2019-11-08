using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
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
        private WeixiaoSysContext _db;
        public MenuController(WeixiaoSysContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet("Menu")]
        public ActionResult<ResponseDto> Menu()
        {
            var result = new ResponseDto();
            try
            {
                result.data = _mapper.Map<List<Menu>, List<MenuDto>>(menus(null));
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            return result;
        }
        [HttpGet("Menus")]
        public ActionResult<ResponseDto> Menus()
        {
            var result = new ResponseDto();
            try
            {
                result.data = _db.Menu.ToList();
            }
            catch (Exception ex)
            {
                result.status = -1;
                result.msg = ex.Message;
            }
            return result;
        }
        private List<Menu> menus(int? id)
        {
            var results = new List<Menu>();
            results = _db.Menu.Where(o => o.Pid == id).ToList();
            if (results.Count() == 0)
            {
                return new List<Menu>();
            }
            foreach (var result in results)
            {
                result.Children = menus(result.Id);
            }
            return results;
        }

    }
}
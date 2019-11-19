using BasicsApi.Dto;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Service
{
    public class AreaService
    {
        private WeixiaoSysContext db;
        public AreaService(WeixiaoSysContext context)
        {
            db = new WeixiaoSysContext();
        }
        /// <summary>
        /// 根据id获取它和子树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Area>> Areas(int? id)
        {
            var results = new List<Area>();
            results = await db.Area.Where(o => o.Pid == id).ToListAsync();
            if (results.Count() == 0)
            {
                return new List<Area>();
            }
            foreach (var result in results)
            {
                result.Children = await Areas(result.Id);
            }
            return results;
        }
        public async Task<string> GetAreaByIds(string ids){
            var idArr = ids.Split(',').ToList().ConvertAll(Convert.ToInt32).ToList();
            var reslut = "";
            foreach (var item in idArr)
            {
                var area = await db.Area.Where(a => a.Id == item).Select(a => a.District).FirstOrDefaultAsync();
                if (!string.IsNullOrEmpty(area))
                {
                    reslut += area + "/";
                }
            }
            return reslut;
        }
        public async Task<List<SelectDto>> SelectAreas(int? id)
        {
            var results = new List<SelectDto>();
            var menus = await db.Area.Where(o => o.Pid == id).Include(x => x.Children).ToListAsync();
            foreach (var x in menus)
            {
                var dto = new SelectDto()
                {
                    title = x.District,
                    label = x.District,
                    key = x.Id,
                    value = x.Id,
                    isLeaf=true,
                };
                dto.children = await SelectAreas(x.Id);
                if (dto.children!=null && dto.children.Count>0)
                {
                    dto.isLeaf = false;
                }
                results.Add(dto);
            };
            return results;
        }
    }
}

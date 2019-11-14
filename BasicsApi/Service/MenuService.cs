using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BasicsApi.Dto;

namespace BasicsApi.Service
{
    public class MenuService
    {
        private  WeixiaoSysContext db;
        public MenuService(WeixiaoSysContext context)
        {
               db = new WeixiaoSysContext();
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPageDto<List<Menu>>> MenuList(MenuDto dto)
        {
            var resultPage=new ResultPageDto<List<Menu>>();
            //resultPage.pi=page.pi;
            //resultPage.ps=page.ps;
            resultPage.total= await db.Menu.CountAsync();
            var menu=db.Menu.Where(menu=>1==1);
            if(!string.IsNullOrEmpty(dto.Text)&& dto.Text!="ascend"&&dto.Text!="descend"){
                menu=menu.Where(menu=>menu.Text.Contains(dto.Text));
            }
            else if(dto.Text=="ascend"){
                menu=menu.OrderBy(m=>m.Text);
            }
             else if(dto.Text=="ascend"){
               menu= menu.OrderByDescending(m=>m.Text);
            }
            else{}
            resultPage.list = await menu.Skip(dto.ps*(dto.pi-1)).Take(dto.ps).ToListAsync();
            return resultPage;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Menu> MenuById(int id)
        {
            var data= await db.Menu.FirstOrDefaultAsync(o => o.Id == id);
            return data;
        }
        /// <summary>
        /// 根据id获取它和子树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Menu>> Menus(int? id)
        {
            var results = new List<Menu>();
            results =await db.Menu.Where(o => o.Pid == id).ToListAsync();
            if (results.Count() == 0)
            {
                return new List<Menu>();
            }
            foreach (var result in results)
            {
                result.Children =await Menus(result.Id);
            }
            return results;
        }
        public async Task<List<SelectDto>> SelectMenus(int? id)
        {
            var results = new List<SelectDto>();
             var menus=await db.Menu.Where(o => o.Pid == id).Include(x => x.Children).ToListAsync();
             foreach (var x in  menus)
              {
                  var dto = new SelectDto()
                  {
                      title = x.Text,
                      label = x.Text,
                      key = x.Id,
                      children =await SelectMenus(x.Id)
                  };
                  results.Add(dto);
              };
            return results;
        }
        public async Task<int> Add(Menu menu)
        {
            db.Menu.Add(menu);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Edit(Menu menu)
        {
            db.Menu.Update(menu);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Delete(int id)
        {
            var del = await db.Menu.Include(m=>m.Children).FirstOrDefaultAsync(o => o.Id == id);
            if(del.Children.Count>0){
                throw new Exception("请先删除子菜单！");
            }
            db.Menu.Remove(del);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Deletes(List<EntityDto> ids)
        {
            var delId = ids.Select(o=>o.Id).ToArray();
            var deles = await db.Menu.Include(m=>m.Children).Where(o =>delId.Contains(o.Id)).ToListAsync();
            if(deles.Any(o=>o.Children.Count>0 && o.Children.Any(c=>!delId.Contains(c.Id)))){
                throw new Exception("存在未删除的子菜单！");
            }
            db.Menu.RemoveRange(deles);
            return await db.SaveChangesAsync();
        }
    }
}

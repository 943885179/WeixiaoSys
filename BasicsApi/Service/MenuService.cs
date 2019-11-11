using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BasicsApi.Service
{
    public class MenuService
    {
        private static readonly object padlock = new object();
        private static WeixiaoSysContext db;
        public MenuService(WeixiaoSysContext context)
        {
            //if (db == null)
            //{
            //    lock (padlock)
            //    {
            //        if (db == null)
            //        {
            //            db = context;
            //        }
            //    }
            //}
            db = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Menu>> MenuList()
        {
           return await db.Menu.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Menu> MenuById(int id)
        {
            return  await db.Menu.FirstOrDefaultAsync(o=>o.Id==id);
        }
        /// <summary>
        /// 根据id获取它和子树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Menu>> Menus(int? id)
        {
            var results = new List<Menu>();
            results = await db.Menu.Where(o => o.Pid == id).ToListAsync();
            if (results.Count() == 0)
            {
                return new List<Menu>();
            }
            foreach (var result in results)
            {
                result.Children = await Menus(result.Id);
            }
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
            var del = await db.Menu.FirstOrDefaultAsync(o => o.Id == id);
            db.Menu.Remove(del);
            return await db.SaveChangesAsync();
        }
    }
}

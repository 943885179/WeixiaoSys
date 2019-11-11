using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BasicsApi.Service
{
    public class CompanyService
    {
        private static object obj;
        private static WeixiaoSysContext db;
        public CompanyService(WeixiaoSysContext context)
        {
            lock (obj)
            {
                if (db==null)
                {
                    db = context;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Company>> Companys()
        {
           return await db.Company.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Company> CompanyById(int id)
        {
            return  await db.Company.FirstOrDefaultAsync(o=>o.Id==id);
        }
        /// <summary>
        /// 根据id获取它和子树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Company>> Companys(int? id)
        {
            var results = new List<Company>();
            results = await db.Company.Where(o => o.Pid == id).ToListAsync();
            if (results.Count() == 0)
            {
                return new List<Company>();
            }
            foreach (var result in results)
            {
                result.InverseP = await Companys(result.Id);
            }
            return results;
        }
        public async Task<int> Add(Company Company)
        {
            db.Company.Add(Company);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Edit(Company Company)
        {
            db.Company.Update(Company);
            return await db.SaveChangesAsync();
        }
    }
}

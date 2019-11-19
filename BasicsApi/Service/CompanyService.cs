using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BasicsApi.Dto;
using BasicsApi.conmon;

namespace BasicsApi.Service
{
    public class CompanyService
    {

        private WeixiaoSysContext db;
        public CompanyService(WeixiaoSysContext context)
        {
            db = new WeixiaoSysContext();
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPageDto<List<Company>>> CompanyList(CompanyDto dto)
        {
            var resultPage = new ResultPageDto<List<Company>>();
            //resultPage.pi=page.pi;
            //resultPage.ps=page.ps;
            resultPage.total = await db.Company.Include(o=>o.CompanyLog).CountAsync();
            var Company = db.Company.Where(Company => 1 == 1);
            if (!string.IsNullOrEmpty(dto.Name) && dto.Name != "ascend" && dto.Name != "descend")
            {
                Company = Company.Where(Company => Company.Name.Contains(dto.Name));
            }
            else if (dto.Name == "ascend")
            {
                Company = Company.OrderBy(m => m.Name);
            }
            else if (dto.Name == "ascend")
            {
                Company = Company.OrderByDescending(m => m.Name);
            }
            else { }
            resultPage.list = await Company.Skip(dto.ps * (dto.pi - 1)).Take(dto.ps).ToListAsync();
            return resultPage;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Company> CompanyById(int id)
        {
            var data = await db.Company.FirstOrDefaultAsync(o => o.Id == id);
            return data;
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
                result.Children = await Companys(result.Id);
            }
            return results;
        }
        public async Task<List<SelectDto>> SelectCompanys(int? id)
        {
            var results = new List<SelectDto>();
            var s = db.Company.Where(o => o.Pid == id || (id == null && o.Pid == 0));
            var Companys = await db.Company.Where(o => o.Pid == id || (id==null && o.Pid==0)).Include(x => x.Children).ToListAsync();
            foreach (var x in Companys)
            {
                var dto = new SelectDto()
                {
                    title = x.Name,
                    label = x.Name,
                    key = x.Id,
                    children = await SelectCompanys(x.Id)
                };
                results.Add(dto);
            };
            return results;
        }
        public async Task<int> Add(Company company)
        {
            //Company.Pid=Company.Pid == 0 ? null : Company.Pid;
            db.Company.Add(company);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Edit(Company company)
        {
            if (company.Pid != null && company.Pid != 0)
            {
                var cIds = await this.CIds(company.Id);
                if (cIds.Contains(company.Pid ?? 1))
                {
                    throw new WeixiaoException("上级不可为该项的子集或者本身!");
                }
            }
            db.Company.Update(company);
            return await db.SaveChangesAsync();
        }
        private async Task<List<int>> CIds(int id){
            var result = new List<int>();
            var company =await db.Company.Include(x=>x.Children).AsNoTracking().FirstOrDefaultAsync(o=>o.Id==id);
            if (company!=null)
            {
                result.Add(company.Id);
            }
            foreach (var x in company.Children)
            {
                 result.AddRange(await CIds(x.Id));
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var del = await db.Company.Include(m => m.Children).FirstOrDefaultAsync(o => o.Id == id);
            if (del.Children.Count > 0)
            {
                throw new WeixiaoException("请先删除子公司！");
            }
            db.Company.Remove(del);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Deletes(List<EntityDto> ids)
        {
            var delId = ids.Select(o => o.Id).ToArray();
            var deles = await db.Company.Include(m => m.Children).Where(o => delId.Contains(o.Id)).ToListAsync();
            if (deles.Any(o => o.Children.Count > 0 && o.Children.Any(c => !delId.Contains(c.Id))))
            {
                throw new WeixiaoException("存在未删除的子公司！");
            }
            db.Company.RemoveRange(deles);
            return await db.SaveChangesAsync();
        }
    }
}

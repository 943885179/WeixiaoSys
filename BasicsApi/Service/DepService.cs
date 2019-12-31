using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using BasicsApi.Dto;
using System;
using BasicsApi.conmon;

namespace BasicsApi.Service
{
    public class DepService
    {
        private WeixiaoSysContext db;
        public DepService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async Task<List<Department>> Departments()
        {
            return await db.Department.Where(o=>o.IsDel!=true).ToListAsync();
        }
        public async Task<Department> DepartmentById(int id)
        {
            return await db.Department.Include(x=>x.Company).FirstAsync(x=>x.Id==id);
        }
        public async Task<int> AddOrEdit(Department department){
            department.IsDel = false;
            if (department.Id==0)
            {
                return await Add(department);
            }
            else
            {
                return await Edit(department);
            }
        }
        private async Task<int> Add(Department department)
        {
            await db.Department.AddAsync(department);
            return await db.SaveChangesAsync();
        }

        public async Task<ResultPageDto<List<Department>>> DepLists(DepDto dto)
        {
            var resultPage = new ResultPageDto<List<Department>>();
            resultPage.total = await db.Department.Where(o=>o.IsDel!=true).CountAsync();
            var dep = db.Department.Where(o=>o.IsDel!=true);
            if (!string.IsNullOrEmpty(dto.DepName) && dto.DepName != "ascend" && dto.DepName != "descend")
            {
                dep = dep.Where(Company => Company.DepName.Contains(dto.DepName));
            }
            else if (dto.DepName == "ascend")
            {
                dep = dep.OrderBy(m => m.DepName);
            }
            else if (dto.DepName == "ascend")
            {
                dep = dep.OrderByDescending(m => m.DepName);
            }
            else { }
            resultPage.list = await dep.Skip(dto.ps * (dto.pi - 1)).Take(dto.ps).ToListAsync();
            return resultPage;
        }

        private async Task<int> Edit(Department department)
        {
            db.Department.Update(department);
            return await db.SaveChangesAsync();
        }
        public async Task<List<SelectDto>> SelectDeps(int? id)
        {
            var results = new List<SelectDto>();
            var Companys =  db.Department.Where(o=>o.IsDel!=true).Where(o => o.Pid == id || (id == null && o.Pid == 0)).Include(x => x.Children).AsAsyncEnumerable();
           await foreach (var x in Companys)
            {
                var dto = new SelectDto()
                {
                    title = x.DepName,
                    label = x.DepName,
                    key = x.Id,
                    children = await SelectDeps(x.Id)
                };
                results.Add(dto);
            };
            return results;
        }

        public async Task<int> Deletes(List<EntityDto> ids)
        {

            var delId = ids.Select(o => o.Id).ToArray();
            var deles = await db.Department.Include(m => m.Children).Where(o => delId.Contains(o.Id)).ToListAsync();
            if (deles.Any(o => o.Children.Where(o=>o.IsDel!=true).ToList().Count > 0 && o.Children.Any(c => !delId.Contains(c.Id))))
            {
                throw new WeixiaoException("存在未删除的子菜单！");
            }
            db.Department.RemoveRange(deles);
            return await db.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var dep = await db.Department.Include(m => m.Children).FirstOrDefaultAsync(o => o.Id == id);
            if (dep.Children.Where(o=>o.IsDel!=true).ToList().Count > 0)
            {
                throw new WeixiaoException("请先删除子部门！");
            }
            dep.IsDel = true;
            return await db.SaveChangesAsync();
        }
    }
}

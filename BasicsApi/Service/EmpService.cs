using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using BasicsApi.Dto;
using BasicsApi.conmon;
using AutoMapper;

namespace BasicsApi.Service
{
    public class EmpService
    {
        private WeixiaoSysContext db;
        public EmpService(WeixiaoSysContext context)
        {
            db = context;
        }
        public async Task<Employee> Login(LoginDto user)
        {
            var result = await db.Employee.Where(o => o.LoginName == user.LoginName && o.LoginPwd == user.LoginPwd).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new WeixiaoException("用户名或密码错误");
            }
            return result;
        }
        public async Task<List<Employee>> Employees()
        {
            return await db.Employee.ToListAsync();
        }
        public async Task<ResultPageDto<List<Employee>>> EmpLists(EmployeeDto dto)
        {
            var resultPage = new ResultPageDto<List<Employee>>();
            resultPage.total = await db.Employee.Where(o => o.Isuse != false).CountAsync();
            var Emp = db.Employee.Include(x => x.Dep).Include(x => x.Dep.Company).Include(x => x.EmpRole).ThenInclude(x => x.Role).Where(o => o.Isuse != false);
            if (!string.IsNullOrEmpty(dto.Name) && dto.Name != "ascend" && dto.Name != "descend")
            {
                Emp = Emp.Where(Company => Company.Name.Contains(dto.Name));
            }
            else if (dto.Name == "ascend")
            {
                Emp = Emp.OrderBy(m => m.Name);
            }
            else if (dto.Name == "ascend")
            {
                Emp = Emp.OrderByDescending(m => m.Name);
            }
            else { }
            resultPage.list = await Emp.Skip(dto.ps * (dto.pi - 1)).Take(dto.ps).ToListAsync();
            return resultPage;
        }
        public async Task<Employee> EmployeeById(int id)
        {
            return await db.Employee.Include(x => x.Dep).Include(x => x.Dep.Company).FirstAsync(x => x.Id == id);
        }
        public async Task<List<Employee>> EmployeeByDepId(int depId)
        {
            return await db.Employee.Include(x => x.Dep).Include(x => x.Dep.Company).Where(x => x.DepId == depId).ToListAsync();
        }
        public async Task<int> AddOrEdit(Employee employee)
        {
            if (employee.Id == 0)
            {
                await Add(employee);
            }
            else
            {
                await Edit(employee);
            }
            return await db.SaveChangesAsync();

        }
        private async Task<int> Add(Employee employee)
        {
            employee.Isuse = true;
            await db.Employee.AddAsync(employee);
            return await db.SaveChangesAsync();
        }
        private async Task<int> Edit(Employee employee)
        {
            db.Employee.Update(employee);
            var roleIds = employee.EmpRole.Select(x => x.RoleId).ToList();
            var removeRole =await db.EmpRole.Where(x => x.EmpId == employee.Id && !roleIds.Contains(x.RoleId)).ToListAsync();
            db.RemoveRange(removeRole);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Delete(int id)
        {
            var dep = await db.Employee.FirstOrDefaultAsync(o => o.Id == id);
            dep.Isuse = false;
            return await db.SaveChangesAsync();
        }
    }
}

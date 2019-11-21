using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasicsApi.Service
{
    public class EmpService
    {
        private WeixiaoSysContext db;
        public EmpService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async  Task<List<Employee>> Employees(){
           return await db.Employee.ToListAsync();
        }
        public async  Task<int> Add(Employee Employee){
            await db.Employee.AddAsync(Employee);
            return await db.SaveChangesAsync();
        }
        public async Task<Employee> EmployeeById(int id){
            return await db.Employee.FindAsync(id);
        }
    }
}

using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using BasicsApi.Dto;
using BasicsApi.conmon;

namespace BasicsApi.Service
{
    public class RoleService
    {
        private WeixiaoSysContext db;
        public RoleService(WeixiaoSysContext context)
        {
            db = context;
        }

        public async Task<List<Role>> Roles()
        {
            return await db.Role.ToListAsync();
        }
        public async Task<List<Role>> RolesByUser(int uid)
        {
            return await db.Role.Where(o => o.EmpRole.Any(t => t.EmpId == uid)).ToListAsync();
        }
        public async Task<int> AddOrEditAsync(RoleDto dto)
        {
            using (var tran = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var dbRole = await db.Role.FindAsync(dto.Id);
                    if (dbRole == null)
                    {
                        dbRole = new Role() { Id = dto.Id, Name = dto.Name };
                        await AddAsync(dbRole);
                    }
                    else
                    {
                        dbRole.Name = dto.Name;
                        await EditAsync(dbRole);
                    }
                    await ChangePoweRole(dbRole.Id, dto.PowerIds);
                    await tran.CommitAsync();
                    return 1;
                }
                catch (System.Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        private async Task<int> ChangePoweRole(int rId, int[] pId)
        {
            var rp = await db.RolePower.Where(x => x.RoleId == rId).ToListAsync();
            var rpIds = rp.Select(x => x.PowerId).ToList();
            var delRps = rp.Where(x => !pId.Contains(x.PowerId)).ToList();
            db.RemoveRange(delRps);
            var addRps = pId.Where(x => !rpIds.Contains(x)).Select(x => new RolePower() { RoleId = rId, PowerId = x }).ToList();
            await db.AddRangeAsync(addRps);
            return await db.SaveChangesAsync();
        }
        private async Task<int> AddAsync(Role role)
        {
            await db.Role.AddAsync(role);
            return await db.SaveChangesAsync();
        }
        private async Task<int> EditAsync(Role role)
        {
            db.Role.Update(role);
            return await db.SaveChangesAsync();
        }
        public async Task<Role> RoleById(int id)
        {
            return await db.Role.FindAsync(id);
        }
        public Task<List<SelectDto>> SelectRoles()
        {
            return db.Role.Select(x => new SelectDto()
            {
                key = x.Id,
                value = x.Id,
                title = x.Name,
                label = x.Name
            }).ToListAsync();
        }

        public async Task<ResultPageDto<List<Role>>> RoleList(RoleDto dto)
        {
            var resultPage = new ResultPageDto<List<Role>>();
            //resultPage.pi=page.pi;
            //resultPage.ps=page.ps;
            resultPage.total = await db.Role.CountAsync();
            if (resultPage.total == 0)
            {
                return resultPage;
            }
            var Role = db.Role.Include(x => x.RolePower).ThenInclude(x => x.Power).Where(o => 1 == 1);
            if (!string.IsNullOrEmpty(dto.Name) && dto.Name != "ascend" && dto.Name != "descend")
            {
                Role = Role.Where(Company => Company.Name.Contains(dto.Name));
            }
            else if (dto.Name == "ascend")
            {
                Role = Role.OrderBy(m => m.Name);
            }
            else if (dto.Name == "ascend")
            {
                Role = Role.OrderByDescending(m => m.Name);
            }
            else { }
            resultPage.list = await Role.Skip(dto.ps * (dto.pi - 1)).Take(dto.ps).ToListAsync();
            return resultPage;
        }
        public async Task<int> Delete(int id)
        {
            var role = await db.Role.FindAsync(id);
            if (role.EmpRole.Count > 0 || role.UsergroupRole.Count > 0)
            {
                throw new WeixiaoException("角色已经被分配使用,不能删除");
            }
            db.Remove(role);
            return await db.SaveChangesAsync();
        }
    }
}

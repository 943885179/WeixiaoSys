using System.Linq;
using BasicsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using BasicsApi.Dto;

namespace BasicsApi.Service
{
    public class PowerService
    {
        private WeixiaoSysContext db;
        public PowerService(WeixiaoSysContext context)
        {
            db = context;
        }
        public async Task<List<Power>> Powers()
        {
            return await db.Power.ToListAsync();
        }
        private async Task<int> AddAsync(Power power)
        {
            await db.Power.AddAsync(power);
            return await db.SaveChangesAsync();
        }
        public async Task<int> AddOrEditAsync(PowerDto dto)
        {
            using (var tran = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var dbPower = await db.Power.FindAsync(dto.Id);
                    if (dbPower == null)
                    {
                        dbPower = new Power() { Id = dto.Id, Name = dto.Name };
                        await AddAsync(dbPower);
                    }
                    else
                    {
                        dbPower.Name = dto.Name;
                        await EditAsync(dbPower);
                    }
                    await ChangePowerMenu(dbPower.Id, dto.MenuIds);
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
        private async Task<int> ChangePowerMenu(int pId, int[] mId)
        {
            var menus = await db.RoleMenu.Where(x => x.PowerId == pId).ToListAsync();
            var mIds = menus.Select(x => x.MenuId).ToList();
            var delMs = menus.Where(x => !mId.Contains(x.MenuId)).ToList();
            db.RemoveRange(delMs);
            var addRps = mId.Where(x => !mIds.Contains(x)).Select(x => new RoleMenu() { PowerId= pId, MenuId = x }).ToList();
            await db.AddRangeAsync(addRps);
            return await db.SaveChangesAsync();
        }
        private async Task<int> EditAsync(Power power)
        {
            db.Power.Update(power);
            return await db.SaveChangesAsync();
        }
        public async Task<int> Delete(int id)
        {
            var power =await db.Power.FindAsync(id);
            db.Remove(power);
            return await db.SaveChangesAsync();
        }
        public async Task<Power> PowerById(int id)
        {
            return await db.Power.FindAsync(id);
        }

        public  Task<List<SelectDto>> SelectPowers()
        {
          return db.Power.Select(x => new SelectDto()
            {
              key=x.Id,
              value=x.Id,
              title=x.Name,
              label=x.Name
          }).ToListAsync();
        }

        public async Task<ResultPageDto<List<Power>>> PowerList(PowerDto dto)
        {
            var resultPage = new ResultPageDto<List<Power>>();
            //resultPage.pi=page.pi;
            //resultPage.ps=page.ps;
            resultPage.total = await db.Power.CountAsync();
            var power = db.Power.Include(x=>x.RoleMenu).ThenInclude(x=>x.Menu).Where(o => 1==1);
            if (!string.IsNullOrEmpty(dto.Name) && dto.Name != "ascend" && dto.Name != "descend")
            {
                power = power.Where(Company => Company.Name.Contains(dto.Name));
            }
            else if (dto.Name == "ascend")
            {
                power = power.OrderBy(m => m.Name);
            }
            else if (dto.Name == "ascend")
            {
                power = power.OrderByDescending(m => m.Name);
            }
            else { }
            resultPage.list = await power.Skip(dto.ps * (dto.pi - 1)).Take(dto.ps).ToListAsync();
            return resultPage;
        }
    }
}

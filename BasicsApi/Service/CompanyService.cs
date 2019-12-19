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
            resultPage.total = await db.Company.Where(o=>o.IsDel!=true).Include(o => o.CompanyLog).Include(o => o.Shareholder).CountAsync();
            var Company = db.Company.Where(o=>o.IsDel!=true);
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
            results = await db.Company.Where(o=>o.IsDel!=true).Where(o => o.Pid == id).ToListAsync();
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
            var Companys =  db.Company.Where(o=>o.IsDel!=true).Where(o => o.Pid == id || (id == null && o.Pid == 0)).Include(x => x.Children).AsAsyncEnumerable();
           await foreach (var x in Companys)
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
        public async Task<bool> Add(Company company)
        {
            //Company.Pid=Company.Pid == 0 ? null : Company.Pid;
            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    company.IsDel = false;
                    db.Company.Add(company);
                    await db.SaveChangesAsync();
                    db.CompanyLog.Add(new CompanyLog()
                    {
                        UpdateTime = DateTime.Now,
                        Cid = company.Id,
                        Content = "添加公司"
                    }); ;
                    await db.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        public async Task<bool> Edit(Company company, int isShare)
        {
            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    if (company.Pid != null && company.Pid != 0)
                    {
                        var cIds = await this.CIds(company.Id);
                        if (cIds.Contains(company.Pid ?? 1))
                        {
                            throw new WeixiaoException("上级不可为该项的子集或者本身!");
                        }
                    }
                    var msg = "";
                    var oldcom = await db.Company.AsNoTracking().FirstOrDefaultAsync(x => x.Id == company.Id);
                    if (!oldcom.Name.Equals(company.Name))
                    {
                        msg += $"公司名称变更{oldcom.Name}=>{company.Name};";
                    }
                    if (!oldcom.LegalPerson.Equals(company.LegalPerson) || !oldcom.Idcard.Equals(company.Idcard))
                    {
                        msg += $"公司CEO变更{oldcom.LegalPerson}[{oldcom.LegalPerson}]=>{company.LegalPerson}[{company.Idcard}];";
                    }
                    if (!oldcom.Area.Equals(company.Area) || !oldcom.Address.Equals(company.Address))
                    {
                        msg += $"公司地址变更{oldcom.Area}{oldcom.Address}=>{company.Area}{company.Address};";
                    }
                    if (!oldcom.Code.Equals(company.Code))
                    {
                        msg += $"公司编号变更{oldcom.Code}=>{company.Code};";
                    }
                    if (!oldcom.Email.Equals(company.Email))
                    {
                        msg += $"公司编号变更{oldcom.Email}=>{company.Email};";
                    }
                    if (!oldcom.Phone.Equals(company.Phone))
                    {
                        msg += $"公司编号变更{oldcom.Phone}=>{company.Phone};";
                    }
                    if (!oldcom.Briefing.Equals(company.Briefing))
                    {
                        msg += $"公司简介变更{oldcom.Briefing}=>{company.Briefing};";
                    }
                    //获取旧投资人
                    var oldshar = await db.Shareholder.Where(o => o.Cid == company.Id).AsNoTracking().ToListAsync();
                    var oldIdcard = oldshar.Select(o => o.Idcard).ToList();
                    var addIdcard = company.Shareholder.Select(o => o.Idcard).ToList();
                    var addShare = company.Shareholder.Where(n => !oldIdcard.Contains(n.Idcard)).ToList();
                    var removeShare = oldshar.Where(n => !addIdcard.Contains(n.Idcard)).ToList();
                    var up = company.Shareholder.Where(n => oldIdcard.Contains(n.Idcard)).ToList();
                    if (isShare == 1)
                    {
                        foreach (var a in removeShare)
                        {
                            db.Shareholder.Remove(a);
                            msg += $"股东{a.Name}【{a.Idcard}】撤股{a.PayMoney}元，撤股比例{a.Proportion};";
                        }
                    }
                    foreach (var a in addShare)
                    {
                        //a.Cid = company.Id;
                        // db.Shareholder.Add(a);
                        msg += $"添加股东{a.Name}【{a.Idcard}】投资{a.PayMoney}元，占股{a.Proportion};";
                    }
                    foreach (var a in up)
                    {
                        var old = oldshar.FirstOrDefault(o => o.Id == a.Id);
                        if (old.Name != a.Name)
                        {
                            msg += $"修改股东{old.Name}为{a.Name};";
                        }
                        if (old.PayMoney != a.PayMoney)
                        {
                            msg += $"修改股东投资金额{a.Name}{old.PayMoney}为{a.PayMoney};";
                        }
                        if (old.Name != a.Name)
                        {
                            msg += $"修改股东占比{a.Name} {old.Proportion}为{a.Proportion};";
                        }
                        db.Shareholder.Update(a);
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        return true;
                    }
                    db.CompanyLog.Add(new CompanyLog()
                    {
                        UpdateTime = DateTime.Now,
                        Cid = company.Id,
                        Content = msg
                    });
                    // company.Shareholder = null;
                    db.Company.Update(company);
                    await db.SaveChangesAsync();
                    await tran.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }
        private async Task<List<int>> CIds(int id)
        {
            var result = new List<int>();
            var company = await db.Company.Include(x => x.Children).AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (company != null)
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
            if (del.Children.Where(o=>o.IsDel!=true).ToList().Count > 0)
            {
                throw new WeixiaoException("请先删除子公司！");
            }
            del.IsDel = true;
            return await db.SaveChangesAsync();
        }
        public async Task<int> Deletes(List<EntityDto> ids)
        {
            var delId = ids.Select(o => o.Id).ToArray();
            var deles = await db.Company.Include(m => m.Children).Where(o => delId.Contains(o.Id)).ToListAsync();
            if (deles.Any(o => o.Children.Where(o=>o.IsDel!=true).ToList().Count > 0 && o.Children.Any(c => !delId.Contains(c.Id))))
            {
                throw new WeixiaoException("存在未删除的子公司！");
            }
            foreach (var del in deles)
            {
                del.IsDel = true;
            }
            //db.Company.RemoveRange(deles);
            return await db.SaveChangesAsync();
        }
        public async Task<List<CompanyLog>> CompanyLogByCid(int cid)
        {
            return await db.CompanyLog.Where(c => c.Cid == cid).ToListAsync();
        }
        public async Task<List<Shareholder>> ShareholderByCid(int cid)
        {
            return await db.Shareholder.Where(c => c.Cid == cid).ToListAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BasicsApi.Controllers
{
    public class DepController : BacsicsController
    {
        private readonly DepService bll;
        public DepController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            bll = new DepService(db);
        }
        [HttpGet("Dep")]
        public async Task<ResponseDto> Dep()
        {
            result.data = await bll.Departments();
            return result;
        }
        [HttpGet("SelectDep")]
        public async Task<ResponseDto> SelectDep()
        {
            result.data = await bll.SelectDeps(null);
            return result;
        }
         [HttpPost("Deps")]
        public async Task<ResponseDto> Deps(DepDto dto)
        {
            result.data = await bll.DepLists(dto);
            return result;
        }
        [HttpGet("DepById/{id}")]
        public async Task<ResponseDto> DepById(int id)
        {
            result.data = await bll.DepartmentById(id);
            return result;
        }
        [HttpPost("AddOrEditDep")]
        public async Task<ResponseDto> AddOrEditDep(Department Dep)
        {
            result.data = await bll.AddOrEdit(Dep);
            return result;
        }
        [HttpPost("DeleteDep/{id}")]
        public async Task<ResponseDto> DeleteDep(int id)
        {
            result.data = await bll.Delete(id);
            return result;
        }
        [HttpPost("DeleteDeps")]
        public async Task<ResponseDto> DeleteDeps(List<EntityDto> ids)
        {
            result.data = await bll.Deletes(ids);
            return result;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
           var data= await bll.SelectDeps(null);
            result.data =data;
            return result;
        }
         [HttpPost("Deps")]
        public async Task<ResponseDto> Deps(DepDto dto)
        {
            result.data = _mapper.Map<ResultPageDto<List<Department>>, ResultPageDto<List<DepDto>>>(await bll.DepLists(dto));
            var x = JsonConvert.SerializeObject(result);
            return result;
        }
        [HttpGet("DepById/{id}")]
        public async Task<ResponseDto> DepById(int id)
        {
            var data = _mapper.Map<Department,DepDto>(await bll.DepartmentById(id));
            result.data = data;
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

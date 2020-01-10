using AutoMapper;
using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<ResultPageDto<List<Menu>>, ResultPageDto<List<MenuDto>>>();
            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, SelectDto>().ForMember(o => o.children, m => m.MapFrom(t => t.Children));


            CreateMap<ResultPageDto<List<Company>>, ResultPageDto<List<CompanyDto>>>();
            CreateMap<Company, CompanyDto>();
            // .ForMember(m=>m.Area,n=>n.MapFrom(o=>o.Area));
            CreateMap<Company, SelectDto>().ForMember(o => o.children, m => m.MapFrom(t => t.Children))
               // .ForMember(m=>m.title,n=>n.MapFrom(o=>o.Text))
               // .ForMember(m => m.key, n => n.MapFrom(o => o.Id))
               ;

            CreateMap<Area, AreaDto>();

            CreateMap<ResultPageDto<List<Department>>, ResultPageDto<List<DepDto>>>();
            CreateMap<Department, DepDto>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<ResultPageDto<List<Employee>>, ResultPageDto<List<EmployeeDto>>>();

            CreateMap<Role, RoleDto>();
            CreateMap<ResultPageDto<List<Role>>, ResultPageDto<List<RoleDto>>>();

            CreateMap<Power, PowerDto>();
            CreateMap<ResultPageDto<List<Power>>, ResultPageDto<List<PowerDto>>>();
            CreateMap<RolePower, RolePowerDto>();
            CreateMap<RoleMenu, RoleMenuDto>();
        }
    }
}

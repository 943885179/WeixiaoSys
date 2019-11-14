using AutoMapper;
using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs() {
            CreateMap<ResultPageDto<List<Menu>>,ResultPageDto<List<MenuDto>>>();
            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, SelectDto>()
                .ForMember(o=>o.children,m=>m.MapFrom(t=>t.Children))
               // .ForMember(m=>m.title,n=>n.MapFrom(o=>o.Text))
               // .ForMember(m => m.key, n => n.MapFrom(o => o.Id))
               ;

        }
    }
}

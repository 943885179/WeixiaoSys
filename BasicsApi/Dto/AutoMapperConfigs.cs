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
            var map = CreateMap<Menu, MenuDto>();
        }
    }
}

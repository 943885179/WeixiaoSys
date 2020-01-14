using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class PowerDto:PageRequestDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual RoleFile RoleFile { get; set; }
        public int[] MenuIds { get; set; }
        public virtual List<RoleMenuDto> RoleMenu { get; set; }
    }
}

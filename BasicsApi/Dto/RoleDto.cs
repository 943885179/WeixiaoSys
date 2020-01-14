using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class RoleDto:PageRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] PowerIds { get; set; }
        //public virtual ICollection<EmpRole> EmpRole { get; set; }
        public virtual ICollection<RolePowerDto> RolePower { get; set; }


        //public virtual ICollection<UsergroupRole> UsergroupRole { get; set; }
    }
}

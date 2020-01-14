using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class RolePowerDto:PageRequestDto
    {

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PowerId { get; set; }

        public virtual PowerDto Power { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class RoleMenuDto
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int PowerId { get; set; }

        public virtual MenuDto Menu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class AreaDto
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string District { get; set; }
        public int Level { get; set; }
        public virtual List<AreaDto> Children { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Area
    {
        public Area()
        {
            Children = new HashSet<Area>();
        }

        public int Id { get; set; }
        public int? Pid { get; set; }
        public string District { get; set; }
        public int Level { get; set; }

        public virtual Area P { get; set; }
        public virtual ICollection<Area> Children { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class RolePower
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PowerId { get; set; }

        public virtual Power Power { get; set; }
        public virtual Role Role { get; set; }
    }
}

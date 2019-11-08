using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class EmpRole
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int RoleId { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual Role Role { get; set; }
    }
}

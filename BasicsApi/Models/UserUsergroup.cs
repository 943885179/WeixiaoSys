using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class UserUsergroup
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int GroupId { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual EmpGroup Group { get; set; }
    }
}

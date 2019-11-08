using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class EmployePosition
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int PosId { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual Position Pos { get; set; }
    }
}

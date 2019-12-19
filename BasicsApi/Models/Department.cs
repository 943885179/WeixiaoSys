using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
            Children = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string DepName { get; set; }
        public string DepCode { get; set; }
        public int CompanyId { get; set; }
        public int? Pid { get; set; }
        public Nullable<bool> IsDel { get; set; }

        public virtual Company Company { get; set; }
        public virtual Department P { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Department> Children { get; set; }
    }
}

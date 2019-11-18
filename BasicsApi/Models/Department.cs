using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new List<Employee>();
            Children = new List<Department>();
        }

        public int Id { get; set; }
        public string DepName { get; set; }
        public string DepCode { get; set; }
        public int CompanyId { get; set; }
        public int? Pid { get; set; }

        public virtual Company Company { get; set; }
        public virtual Department P { get; set; }
        public virtual List<Employee> Employee { get; set; }
        public virtual List<Department> Children { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmpRole = new List<EmpRole>();
            EmployePosition = new List<EmployePosition>();
            UserUsergroup = new List<UserUsergroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string Idcard { get; set; }
        public string Phone { get; set; }
        public int DepId { get; set; }
        public int? Qq { get; set; }
        public string Wechar { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Img { get; set; }
        public bool? Isuse { get; set; }
        public DateTime Stardate { get; set; }
        public DateTime? Enddate { get; set; }

        public virtual Department Dep { get; set; }
        public virtual List<EmpRole> EmpRole { get; set; }
        public virtual List<EmployePosition> EmployePosition { get; set; }
        public virtual List<UserUsergroup> UserUsergroup { get; set; }
    }
}

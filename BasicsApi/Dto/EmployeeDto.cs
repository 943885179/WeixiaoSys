using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class EmployeeDto:PageRequestDto
    {
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

        public virtual DepDto Dep { get; set; }
        public virtual ICollection<EmpRole> EmpRole { get; set; }
        public virtual ICollection<EmployePosition> EmployePosition { get; set; }
        public virtual ICollection<UserUsergroup> UserUsergroup { get; set; }
    }
}

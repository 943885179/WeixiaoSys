using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyLog = new HashSet<CompanyLog>();
            Department = new HashSet<Department>();
            Children = new HashSet<Company>();
            Shareholder = new HashSet<Shareholder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? Pid { get; set; }
        public string LegalPerson { get; set; }
        public string Idcard { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string Briefing { get; set; }

        public virtual Company P { get; set; }
        public virtual ICollection<CompanyLog> CompanyLog { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Company> Children { get; set; }
        public virtual ICollection<Shareholder> Shareholder { get; set; }
    }
}

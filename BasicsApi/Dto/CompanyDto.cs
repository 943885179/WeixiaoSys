//using Newtonsoft.Json;
using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BasicsApi.Dto
{
    public  class CompanyDto
    {
        public int pi { get; set; }//当前页码
        public int ps { get; set; }//每页数量，当设置为 0 表示不分页，默认：10

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Pid { get; set; }
        public string LegalPerson { get; set; }
        public string Idcard { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }

        public virtual Area AreaNavigation { get; set; }
        public virtual CompanyDto P { get; set; }
        public virtual ICollection<CompanyLog> CompanyLog { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<CompanyDto> Children { get; set; }
        public virtual ICollection<Shareholder> Shareholder { get; set; }
    }
}

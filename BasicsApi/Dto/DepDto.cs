using System;
using System.Collections.Generic;
using BasicsApi.Models;

namespace BasicsApi.Dto
{
    public class DepDto: PageRequestDto
    {
        public int Id { get; set; }
        public string DepName { get; set; }
        public string DepCode { get; set; }
        public int CompanyId { get; set; }
        public int? Pid { get; set; }
        public Nullable<bool> IsDel { get; set; }

        public virtual CompanyDto Company { get; set; }
        public virtual List<Employee> Employee { get; set; }
        public virtual List<DepDto> Children { get; set; }
    }
}

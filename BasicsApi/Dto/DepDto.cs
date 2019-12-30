using System;
using System.Collections.Generic;
using BasicsApi.Models;

namespace BasicsApi.Dto
{
    public class DepDto
    {
        public int pi { get; set; }//当前页码
        public int ps { get; set; }//每页数量，当设置为 0 表示不分页，默认：10

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

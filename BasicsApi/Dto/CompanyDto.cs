//using Newtonsoft.Json;
using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BasicsApi.Dto
{
    public class CompanyDto: PageRequestDto
    {

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
        // public virtual CompanyDto P { get; set; }
        public virtual List<CompanyLog> CompanyLog { get; set; }
       // public virtual List<DepDto> Department { get; set; }
        public virtual List<CompanyDto> Children { get; set; }
        public virtual List<Shareholder> Shareholder { get; set; }
    }
}

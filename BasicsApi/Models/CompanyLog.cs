using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class CompanyLog
    {
        public int Id { get; set; }
        public int Cid { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Content { get; set; }

        public virtual Company C { get; set; }
    }
}

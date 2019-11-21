using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Shareholder
    {
        public int Id { get; set; }
        public int? Cid { get; set; }
        public string Name { get; set; }
        public string Idcard { get; set; }
        public decimal Proportion { get; set; }
        public decimal PayMoney { get; set; }

        public virtual Company C { get; set; }
    }
}

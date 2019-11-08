using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class RoleOpretion
    {
        public int Id { get; set; }
        public int OpeId { get; set; }
        public int PowerId { get; set; }

        public virtual Operation Ope { get; set; }
        public virtual Power Power { get; set; }
    }
}

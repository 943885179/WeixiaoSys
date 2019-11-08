using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class RolePage
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int PowerId { get; set; }

        public virtual Page Page { get; set; }
        public virtual Power Power { get; set; }
    }
}

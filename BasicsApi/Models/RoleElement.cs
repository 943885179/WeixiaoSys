using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class RoleElement
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public int PowerId { get; set; }

        public virtual Element Element { get; set; }
        public virtual Power Power { get; set; }
    }
}

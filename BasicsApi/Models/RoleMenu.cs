using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class RoleMenu
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int PowerId { get; set; }

        public virtual Power IdNavigation { get; set; }
        public virtual Menu Menu { get; set; }
    }
}

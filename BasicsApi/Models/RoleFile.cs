using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class RoleFile
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int PowerId { get; set; }

        public virtual Files File { get; set; }
        public virtual Power IdNavigation { get; set; }
    }
}

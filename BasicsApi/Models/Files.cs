using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Files
    {
        public Files()
        {
            RoleFile = new HashSet<RoleFile>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public virtual ICollection<RoleFile> RoleFile { get; set; }
    }
}

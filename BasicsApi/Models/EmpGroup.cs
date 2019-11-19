using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class EmpGroup
    {
        public EmpGroup()
        {
            UserUsergroup = new HashSet<UserUsergroup>();
            UsergroupRole = new HashSet<UsergroupRole>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<UserUsergroup> UserUsergroup { get; set; }
        public virtual ICollection<UsergroupRole> UsergroupRole { get; set; }
    }
}

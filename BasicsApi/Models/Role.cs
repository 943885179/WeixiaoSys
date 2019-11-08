using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Role
    {
        public Role()
        {
            EmpRole = new HashSet<EmpRole>();
            RolePower = new HashSet<RolePower>();
            UsergroupRole = new HashSet<UsergroupRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmpRole> EmpRole { get; set; }
        public virtual ICollection<RolePower> RolePower { get; set; }
        public virtual ICollection<UsergroupRole> UsergroupRole { get; set; }
    }
}

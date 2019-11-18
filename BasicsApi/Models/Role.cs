using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Role
    {
        public Role()
        {
            EmpRole = new List<EmpRole>();
            RolePower = new List<RolePower>();
            UsergroupRole = new List<UsergroupRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<EmpRole> EmpRole { get; set; }
        public virtual List<RolePower> RolePower { get; set; }
        public virtual List<UsergroupRole> UsergroupRole { get; set; }
    }
}

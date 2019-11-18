using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class EmpGroup
    {
        public EmpGroup()
        {
            UserUsergroup = new List<UserUsergroup>();
            UsergroupRole = new List<UsergroupRole>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }

        public virtual List<UserUsergroup> UserUsergroup { get; set; }
        public virtual List<UsergroupRole> UsergroupRole { get; set; }
    }
}

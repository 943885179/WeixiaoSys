using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class UsergroupRole
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }

        public virtual EmpGroup Group { get; set; }
        public virtual Role Role { get; set; }
    }
}

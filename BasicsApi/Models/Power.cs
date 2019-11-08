using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Power
    {
        public Power()
        {
            RoleElement = new HashSet<RoleElement>();
            RoleOpretion = new HashSet<RoleOpretion>();
            RolePage = new HashSet<RolePage>();
            RolePower = new HashSet<RolePower>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual RoleFile RoleFile { get; set; }
        public virtual RoleMenu RoleMenu { get; set; }
        public virtual ICollection<RoleElement> RoleElement { get; set; }
        public virtual ICollection<RoleOpretion> RoleOpretion { get; set; }
        public virtual ICollection<RolePage> RolePage { get; set; }
        public virtual ICollection<RolePower> RolePower { get; set; }
    }
}

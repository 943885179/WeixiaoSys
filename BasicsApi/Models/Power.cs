using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Power
    {
        public Power()
        {
            RoleElement = new List<RoleElement>();
            RoleOpretion = new List<RoleOpretion>();
            RolePage = new List<RolePage>();
            RolePower = new List<RolePower>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual RoleFile RoleFile { get; set; }
        public virtual RoleMenu RoleMenu { get; set; }
        public virtual List<RoleElement> RoleElement { get; set; }
        public virtual List<RoleOpretion> RoleOpretion { get; set; }
        public virtual List<RolePage> RolePage { get; set; }
        public virtual List<RolePower> RolePower { get; set; }
    }
}

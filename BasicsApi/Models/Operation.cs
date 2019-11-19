using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Operation
    {
        public Operation()
        {
            Element = new HashSet<Element>();
            RoleOpretion = new HashSet<RoleOpretion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Pid { get; set; }

        public virtual ICollection<Element> Element { get; set; }
        public virtual ICollection<RoleOpretion> RoleOpretion { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Operation
    {
        public Operation()
        {
            Element = new List<Element>();
            RoleOpretion = new List<RoleOpretion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Pid { get; set; }

        public virtual List<Element> Element { get; set; }
        public virtual List<RoleOpretion> RoleOpretion { get; set; }
    }
}

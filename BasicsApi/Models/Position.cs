using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Position
    {
        public Position()
        {
            EmployePosition = new List<EmployePosition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Describes { get; set; }

        public virtual List<EmployePosition> EmployePosition { get; set; }
    }
}

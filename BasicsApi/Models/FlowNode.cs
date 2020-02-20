using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowNode
    {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Label { get; set; }
    }
}

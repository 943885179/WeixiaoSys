using System.Collections.Generic;

namespace BasicsApi.Models
{
    public class FlowData:WeixiaoEntity
    {
        public List<FlowNode> Nodes { get; set; }
        public List<FlowEdge> Edges { get; set; }
        public List<FlowGroup> Groups { get; set; }
    }
}

using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class G6ResultDto
    {
        public List<RegisterBehavior> RegisterBehaviors { get; set; }
        public List<RegisterEdge> RegisterEdges { get; set; }
        public FlowGraph FlowGraph { get; set; }
        public FlowData FlowData { get; set; }
        public FlowStyle FlowStyle { get; set; }
        /// <summary>
        /// 方法设置
        /// </summary>
        public List<FlowFun> Ons { get; set; }
    }
    public class FlowData
    {
        public List<FlowNode> Nodes { get; set; }
        public List<FlowEdge> Edges { get; set; }
        public List<FlowGroup> Groups { get; set; }
    }
}

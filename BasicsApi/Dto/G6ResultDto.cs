using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class G6ResultDto
    {
        public FlowGraph FlowGraph { get; set; }
        public FlowData FlowData { get; set; }
        /// <summary>
        /// 方法设置
        /// </summary>
        public List<FlowFun> Ons { get; set; }
    }
    public class FlowData
    {
        public List<FlowNode> Nodes { get; set; }
        public List<FlowEdge> Edges { get; set; }
    }
}

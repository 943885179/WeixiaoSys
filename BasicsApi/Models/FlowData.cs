using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public class FlowData : WeixiaoEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Gid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        public List<FlowNode> Nodes { get; set; }
        public List<FlowEdge> Edges { get; set; }
        public List<FlowGroup> Groups { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowG6:WeixiaoEntity
    {
        /// <summary>
        /// 前置方法（创建菜单等）
        /// </summary>
        public List<FlowFun> FlowFronts { get; set; }
        public List<FlowRegisterBehavior> RegisterBehaviors { get; set; }
        public List<FlowRegisterEdge> RegisterEdges { get; set; }
        public FlowGraph FlowGraph { get; set; }
        public FlowData FlowData { get; set; }
        public FlowCss FlowCss { get; set; }
        /// <summary>
        /// 方法设置
        /// </summary>
        public List<FlowFun> Ons { get; set; }
    }
}

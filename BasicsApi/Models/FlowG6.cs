using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowG6 : WeixiaoEntity
    {
        /// <summary>
        /// 前置方法（创建菜单等）
        /// </summary>
        //[InverseProperty("FlowG6_FlowFronts")] //反向导航
        public virtual List<FlowFun> FlowFronts { get; set; }
        public virtual List<FlowRegisterBehavior> RegisterBehaviors { get; set; }
        public virtual List<FlowRegisterEdge> RegisterEdges { get; set; }
        public virtual FlowGraph FlowGraph { get; set; }
        public virtual FlowData FlowData { get; set; }
        public virtual FlowCss FlowCss { get; set; }
        /// <summary>
        /// 方法设置
        /// </summary>
        //[InverseProperty("FlowG6_Ons")] //反向导航
        public virtual List<FlowFun> Ons { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowEdge: WeixiaoEntity
    {
        /// <summary>
        /// 类型 默认直线line 不支持控制点；.折线 polyline 支持多个控制点, 二阶贝塞尔曲线 quadratic 三阶贝塞尔曲线 cubic,圆弧线 arc,折线自环 loop,cubic-vertical：垂直方向的三阶贝塞尔曲线，不考虑用户从外部传入的控制点；cubic-horizontal；水平方向的三阶贝塞尔曲线，不考虑用户从外部传入的控制点；
        /// </summary>
        public string Shape { get; set; } = "line";
        /// <summary>
        /// 开始节点
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 结束节点
        /// </summary>
        public string Target { get; set; }

        public string Label { get; set; }
        public FlowLabelCfgs LabelCfg { get; set; }
        /// <summary>
        /// 边的样式
        /// </summary>
        public FlowStyle Style { get; set; }
        public FlowEdgeLoopCfg LoopCfg { get; set; }
    }
}

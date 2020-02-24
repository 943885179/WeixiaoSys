using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowEdge
    {
        /// <summary>
        /// 类型 默认直线line 不支持控制点；.折线 polyline 支持多个控制点, 二阶贝塞尔曲线 quadratic 三阶贝塞尔曲线 cubic,圆弧线 arc,折线自环 loop,cubic-vertical：垂直方向的三阶贝塞尔曲线，不考虑用户从外部传入的控制点；cubic-horizontal；水平方向的三阶贝塞尔曲线，不考虑用户从外部传入的控制点；
        /// </summary>
        public string Shape { get; set; } = "line";
        public string Source { get; set; }

        public string Target { get; set; }

        public string Label { get; set; }
        public LabelCfgs LabelCfg { get; set; }
        /// <summary>
        /// 边的样式
        /// </summary>
        public Style Style { get; set; }
        public LoopCfg LoopCfg { get; set; }
    }
    public class LoopCfg
    {
        /// <summary>
        /// position: 指定自环与节点的相对位置。默认为：top。支持的值有：top, top-right, right,bottom-right, bottom, bottom-left, left, top-left
        /// </summary>
        public string Position { get; set; } = "top";
        /// <summary>
        /// dist: 从节点 keyShape 的边缘到自环最顶端的位置，用于指定自环的曲度，默认为节点的高度。
        /// </summary>
        public int Dist { get; set; }
        /// <summary>
        /// clockwise: 指定是否顺时针画环，默认为 true。
        /// </summary>
        public bool Clockwise { get; set; } = true;
    }
}

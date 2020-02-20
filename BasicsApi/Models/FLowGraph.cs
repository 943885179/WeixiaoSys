﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FLowGraph
    {
        /// <summary>
        /// 图的 DOM 容器，可以传入该 DOM 的 id 或者直接传入容器的 HTML 节点对象
        /// </summary>
        public string Container { get; set; } = "mountNode";
        /// <summary>
        /// 长度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 默认：canvas 渲染引擎，支持 canvas 和 SVG。
        /// </summary>
        public string Renderer { get; set; } = "canvas";
        /// <summary>
        /// 是否开启画布自适应
        /// </summary>
        public bool FitView { get; set; } = false;
        /// <summary>
        /// Array | Number
        /// fitView 为 true 时生效。图适应画布时，指定四周的留白。可以是一个值, 
        /// 例如：fitViewPadding: 20也可以是一个数组，例如：fitViewPadding: [ 20, 40, 50, 20 ]
        /// 当指定一个值时，四边的边距都相等，当指定数组时，数组内数值依次对应 上，右，下，左四边的边距。
        /// </summary>
        public int[] FitViewPadding
        {
            get
            {
                return this.FitViewPadding;
            }
            set
            {
                switch (value.Length)
                {
                    case 4:
                        this.FitViewPadding = value;
                        break;
                    case 1:
                        this.FitViewPadding = new int[4] { value[0], value[0], value[0], value[0] };
                        break;
                    case 2:
                        this.FitViewPadding = new int[4] { value[0], value[1], value[0], value[1] };
                        break;
                    default:
                        this.FitViewPadding = new int[4] { 0, 0, 0, 0 };
                        break;
                }
            }
        }
        /// <summary>
        ///默认true, 各种元素是否在一个分组内，决定节点和边的层级问题，默认情况下所有的节点在一个分组中，所有的边在一个分组中，当这个参数为 false 时，节点和边的层级根据生成的顺序确定。
        /// </summary>
        public bool GroupByTypes { get; set; } = true;
        /// <summary>
        /// 节点分组类型，支持 circle 和 rect
        /// </summary>
        public string GoupType { get; set; } = "circle";
        /// <summary>
        /// 默认true 当图中元素更新，或视口变换时，是否自动重绘。建议在批量操作节点时关闭，以提高性能，完成批量操作后再打开，参见后面的 setAutoPaint() 方法。
        /// </summary>
        public bool AitoPaint { get; set; } = true;
        /// <summary>
        /// 最小缩放
        /// </summary>
        public double MinZoom { get; set; } = 0.2;
        /// <summary>
        /// 最大缩放
        /// </summary>
        public double MaxZoom { get; set; } = 10;
    }
}

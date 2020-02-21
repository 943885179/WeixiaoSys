using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
       // private int[] fitViewPadding;
        /// <summary>
        /// Array | Number
        /// fitView 为 true 时生效。图适应画布时，指定四周的留白。可以是一个值,
        /// 例如：fitViewPadding: 20也可以是一个数组，例如：fitViewPadding: [ 20, 40, 50, 20 ]
        /// 当指定一个值时，四边的边距都相等，当指定数组时，数组内数值依次对应 上，右，下，左四边的边距。
        /// </summary>
        public int[] FitViewPadding { get; set; }
        //{
        //    get
        //    {
        //        return this.fitViewPadding;
        //    }
        //    set
        //    {
        //        switch (value.Length)
        //        {
        //            case 4:
        //                this.fitViewPadding = value;
        //                break;
        //            case 1:
        //                this.fitViewPadding = new int[4] { value[0], value[0], value[0], value[0] };
        //                break;
        //            case 2:
        //                this.fitViewPadding = new int[4] { value[0], value[1], value[0], value[1] };
        //                break;
        //            default:
        //                this.fitViewPadding = new int[4] { 0, 0, 0, 0 };
        //                break;
        //        }
        //    }
        //}
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
        /// <summary>
        /// 默认节点样式
        /// </summary>
        public LabelCfgs DefaultNode { get; set; }
        /// <summary>
        /// 默认线路样式
        /// </summary>
        public LabelCfgs DefaultEdge { get; set; }
        public Mode Modes { get; set; }
        /// <summary>
        /// 样式node可以不设置下x，y，按照规则生成（会让设置了x，y的也失效）,不实现的话则会重叠在0，0位置
        /// </summary>
        public Layout Layout { get; set; }
    }

    public class Layout
    {
        /// <summary>
        /// 必须使用小写，大小写敏感
        /// 一般图：
        ///• Random Layout：随机布局；
        ///• Force Layout：经典力导向布局：
        ///力导向布局：一个布局网络中，粒子与粒子之间具有引力和斥力，从初始的随机无序的布局不断演变，逐渐趋于平衡稳定的布局方式称之为力导向布局。适用于描述事物间关系，比如人物关系、计算机网络关系等。
        ///• Circular Layout：环形布局；
        ///• Radial Layout：辐射状布局；
        ///• MDS Layout：高维数据降维算法布局；
        ///• Fruchterman Layout：Fruchterman 布局，一种力导布局；
        ///• Dagre Layout：层次布局。
        ///树图布局：
        ///• Dendrogram Layout：树状布局（叶子节点布局对齐到同一层）；
        ///• CompactBox Layout：紧凑树布局；
        ///• Mindmap Layout：脑图布局；
        ///• Intended Layout：缩进布局。
        /// </summary>
        public string Type { get; set; } = "random";
        /// <summary>
        /// preventOverlap  防止节点重叠
        /// </summary>
        public bool PreventOverlap { get; set; } = true;
        /// <summary>
        /// 指定边距离
        /// </summary>
        public int LinkDistance { get; set; }
    }
    public class Mode
    {
        /// <summary>
        /// drag-canvas
        /// </summary>
        //public string[] Default { get; set; }
        // [JsonProperty("Default")]
        // [System.Text.Json.Serialization.JsonPropertyName("Default")]
        public List<ModeType> Default { get; set; }
        //[JsonProperty("Edit")]
        //[System.Text.Json.Serialization.JsonPropertyName("Edit")]
        public List<ModeType> Edit { get; set; }
        // public string[] Edit { get; set; }
    }

    public class ModeType
    {
        /// <summary>
        /// 拖拽画布type: 'drag-canvas'
        /// 缩放画布type: 'zoom-canvas'
        /// 拖拽节点type: 'drag-node'
        /// 点击选中节点，再次点击节点或点击 Canvas 取消选中状态type: 'click-select'
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// direction: 允许拖拽方向，支持'x'， 'y'，'both'，默认方向为 'both'。
        /// </summary>
        public string Direction { get; set; } = "both";
        /// <summary>
        /// • sensitivity: 缩放灵敏度，支持 1-10 的数值，默认灵敏度为 5。 提示：若要限定缩放尺寸，请在 graph 上设置 minZoom 和 maxZoom。
        /// </summary>
        public int Sensitivity { get; set; } = 5;
        /// <summary>
        /// delegateStyle: 节点拖拽时的绘图属性，默认为 { strokeOpacity: 0.6, fillOpacity: 0.6 }；
        /// </summary>
        public DelegateStyle DelegateStyle { get; set; } = new DelegateStyle();
        /// <summary>
        /// updateEdge: 是否在拖拽节点时更新所有与之相连的边，默认为 true 。
        /// </summary>
        public bool UpdateEdge { get; set; } = true;
        /// <summary>
        /// • 3.1.2 enableDelegate：拖动节点过程中是否启用 delegate，即在拖动过程中是否使用方框代替元素的直接移动，效果区别见下面两个动图。默认值为 false。
        /// </summary>
        public bool EnableDelegate { get; set; } = false;
    }
    public class DelegateStyle
    {
        //strokeOpacity: 0.6, fillOpacity: 0.6
        public double StrokeOpacity { get; set; } = 0.6;
        public double FillOpacity { get; set; } = 0.6;
    }
}

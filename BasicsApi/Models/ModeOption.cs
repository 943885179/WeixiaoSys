using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public abstract class ModeOption
    {
        protected ModeOption()
        {
        }

        /// <summary>
        /// 拖拽画布type: 'drag-canvas'
        /// 缩放画布type: 'zoom-canvas'
        /// 拖拽节点type: 'drag-node'
        /// 点击选中节点，再次点击节点或点击 Canvas 取消选中状态type: 'click-select'
        /// </summary>
        public virtual string Type { get; set; }
    }
    /// <summary>
    /// 拖拽画布
    /// </summary>
    public class DragCanvas : ModeOption
    {
        public DragCanvas()
        {
        }

        /// <summary>
        /// drag-canvas
        /// </summary>
        public override string Type { get => "drag-canvas"; set => base.Type = value; }
        /// <summary>
        /// direction: 允许拖拽方向，支持'x'， 'y'，'both'，默认方向为 'both'。
        /// </summary>
        public string Direction { get; set; } = "both";
    }
    /// <summary>
    /// 缩放画布
    /// </summary>
    public class ZoomCanvas : ModeOption
    {
        public ZoomCanvas()
        {
        }

        /// <summary>
        /// zoom-canvas
        /// </summary>
        public override string Type { get => "zoom-canvas"; set => base.Type = value; }
        /// <summary>
        /// • sensitivity: 缩放灵敏度，支持 1-10 的数值，默认灵敏度为 5。 提示：若要限定缩放尺寸，请在 graph 上设置 minZoom 和 maxZoom。
        /// </summary>
        public int Sensitivity { get; set; } = 5;

    }
    /// <summary>
    /// 拖拽节点
    /// </summary>
    public class DragNode : ModeOption
    {
        public DragNode()
        {
        }

        /// <summary>
        /// drag-node 拖拽节点
        /// </summary>
        public override string Type { get => "drag-node"; set => base.Type = value; }
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
    /// <summary>
    ///  delegateStyle: 节点拖拽时的绘图属性，默认为 { strokeOpacity: 0.6, fillOpacity: 0.6 }；
    /// </summary>
    public class DelegateStyle
    {
        public DelegateStyle()
        {
        }

        //strokeOpacity: 0.6, fillOpacity: 0.6
        public double StrokeOpacity { get; set; } = 0.6;
        public double FillOpacity { get; set; } = 0.6;
    }
    /// <summary>
    /// 点击选中节点，再次点击节点或点击 Canvas 取消选中状态
    /// </summary>
    public class ClickSelect : ModeOption
    {
        public ClickSelect()
        {
        }

        /// <summary>
        /// click-select
        /// </summary>
        public override string Type { get => "click-select"; set => base.Type = value; }
        /// <summary>
        /// 是否允许多选，默认为 true，当设置为 false，表示不允许多选，此时 trigger 参数无效。
        /// </summary>
        public bool Multiple { get; set; } = true;
        /// <summary>
        /// 3.1.2 trigger: 指定按住哪个键进行多选，默认为 shift，按住 Shift 键多选，用户可配置 shift、ctrl、alt；
        /// </summary>
        public string Trigger { get; set; } = "shift";
    }
    /// <summary>
    /// 节点文本提示
    /// </summary>
    public class Tooltip : ModeOption
    {
        public Tooltip()
        {

        }
        /// <summary>
        /// tooltip
        /// </summary>
        public override string Type { get => "tooltip"; set => base.Type = value; }
        /// <summary>
        /// tooltip 提示开启时
        /// </summary>
        public string FormatText { get; set; } = "(model)=>{return '<div style=\"padding: 10px 6px;color:red;background-color: rgba(255,255,255,0.7);border: 1px solid #e2e2e2;border-radius: 4px;\">'+model.label+'</div>';}";

    }
    /// <summary>
    /// 边文本提示 使用方式基本与 tooltip 相同，但是移到边时触发。主要是为了将两个交互区分开，以满足用户边与节点的提示样式或 HTML 结构不同，以及不需要在事件中去区分是节点事件还是边事件等。
    /// </summary>
    public class EdgeTooltip : ModeOption
    {
        public EdgeTooltip()
        {
        }

        /// <summary>
        /// tooltip
        /// </summary>
        public override string Type { get => "edge-tooltip"; set => base.Type = value; }
        /// <summary>
        /// tooltip 提示开启时
        /// </summary>
        public string FormatText { get; set; } = "(model)=>{return '<div style=\"padding: 10px 6px;color:red;background-color: rgba(255,255,255,0.7);border: 1px solid #e2e2e2;border-radius: 4px;\">'+model.label+'</div>';}";

    }
    /// <summary>
    /// activate-relations 当鼠标移到某节点时，突出显示该节点以及与其直接关联的节点和连线；
    /// </summary>
    public class ActivateRelations : ModeOption
    {
        public ActivateRelations()
        {
        }

        /// <summary>
        ///  activate-relations
        /// </summary>
        public override string Type { get => "activate-relations"; set => base.Type = value; }
        /// <summary>
        /// trigger: 'mouseenter', 可以是 mousenter , 鼠标移入时触发；也可以是 click ，鼠标点击时触发
        /// </summary>
        public string Trigger { get; set; } = "mouseenter";
        /// <summary>
        /// activeState: 'active', 活跃节点状态；当行为被触发，需要被突出显示的节点和边都会附带此状态，默认值为 active；可以与 graph 实例的 nodeStyle 和 edgeStyle 结合实现丰富的视觉效果。
        /// </summary>
        public string ActiveState { get; set; } = "active";
        /// <summary>
        /// inactiveState: 'inactive'，非活跃节点状态，不需要被突出显示的节点和边都会附带此状态，默认值为 inactive；可以与 graph 实例的 nodeStyle 和 edgeStyle 结合实现丰富的视觉效果；
        /// </summary>
        public string InactiveState { get; set; } = "inactive";
        /// <summary>
        /// 3.1.2 resetSelected：高亮相连节点时是否重置已经选中的节点，默认为false，即选中的节点状态不会被 activate-relations 覆盖。
        /// </summary>
        public bool ResetSelected { get; set; } = false;
    }
    /// <summary>
    /// brush-select 拖动框选节点
    /// </summary>
    public class BrushSelect : ModeOption
    {
        public BrushSelect()
        {
        }

        /// <summary>
        /// brush-select
        /// </summary>
        public override string Type { get => "brush-select"; set => base.Type = value; }
        /// <summary>
        /// brushStyle：拖动框选框的样式；
        /// </summary>
        public string BrushStyle { get; set; }
        /// <summary>
        /// onSelect(nodes)：选中节点时的回调，参数 nodes 表示选中的节点；
        /// </summary>
        public string OnSelect { get; set; }
        /// <summary>
        /// onDeselect(nodes)：取消选中节点时的回调，参数 nodes 表示取消选中的节点；
        /// </summary>
        public string OnDeselect { get; set; }
        /// <summary>
        ///  brushStyle：框选时样式的配置项，包括 fill、fillOpacity、stroke 和 lineWidth 四个属性；
        /// </summary>
        public string BrushStyles { get; set; }
        /// <summary>
        /// selectedState：选中的状态，默认值为 'selected'；
        /// </summary>
        public string SelectedState { get; set; } = "selected";
        /// <summary>
        ///  includeEdges：框选过程中是否选中边，默认为 true，用户配置为 false 时，则不选中边；
        /// </summary>
        public bool IncludeEdges { get; set; } = true;
        /// <summary>
        ///  3.1.2 trigger：触发框选的动作，默认为 'shift'，即用户按住 Shift 键拖动就可以进行框选操作，可配置的的选项为: 'shift'、'ctrl' / 'control'、'alt' 和 'drag' ，不区分大小写
        ///  'shift'：按住 Shift 键进行拖动框选；
        /// • 'ctrl' / 'control'：按住 Ctrl 键进行拖动框选；
        /// • 'alt'：按住 Alt 键进行拖动框选；
        /// • 风险 'drag'：不需要按任何键，进行拖动框选，如果同时配置了 drag-canvas，则会与该选项冲突。
        /// </summary>
        public string Trigger { get; set; } = "shift";
    }
    /// <summary>
    /// collapse-expand 只适用于树图，展开或收起节点
    /// </summary>
    public class CollapseExpand : ModeOption
    {
        public CollapseExpand()
        {
        }

        /// <summary>
        /// collapse-expand
        /// </summary>
        public override string Type { get => "collapse-expand"; set => base.Type = value; }
        /// <summary>
        /// trigger：收起和展开树图的方式，支持click和dblclick两种方式，默认为click；
        /// </summary>
        public string Trigger { get; set; } = "click";
        /// <summary>
        /// onChange：收起或展开的回调函数，警告 3.1.2 版本中将移除。
        /// </summary>
        [Obsolete]
        public string OnChange { get; set; }
    }
    /// <summary>
    /// collapse-expand-group 收起和展开群组
    /// </summary>
    public class CollapseExpandGroup : ModeOption
    {
        public CollapseExpandGroup()
        {
        }

        /// <summary>
        /// collapse-expand-group
        /// </summary>
        public override string Type { get => "collapse-expand-group"; set => base.Type = value; }
        /// <summary>
        /// 3.1.2 trigger：收起和展开节点分组的方式，支持click和dblclick两种方式，默认为dblclick
        /// </summary>
        public string Trigger { get; set; } = "dblclick";
    }
    /// <summary>
    /// drag-group 拖动节点分组
    /// </summary>
    public class DragGroup : ModeOption
    {
        public DragGroup()
        {
        }

        /// <summary>
        /// drag-group
        /// </summary>
        public override string Type { get => "drag-group"; set => base.Type = value; }
        /// <summary>
        ///delegateStyle：拖动节点分组时 delegate 的样式。
        /// </summary>
        public string DelegateStyle { get; set; }
    }
    /// <summary>
    /// drag-node-with-group 拖动节点分组中的节点
    /// </summary>
    public class DragNodeWithGroup : ModeOption
    {
        public DragNodeWithGroup()
        {
        }

        /// <summary>
        /// drag-node-with-group
        /// </summary>
        public override string Type { get => "drag-node-with-group"; set => base.Type = value; }
        /// <summary>
        ///delegateStyle：拖动节点分组时 delegate 的样式。
        /// </summary>
        public string DelegateStyle { get; set; }
        /// <summary>
        /// 最大倍数
        /// </summary>
        public int MaxMultiple { get; set; }
        /// <summary>
        /// 最小倍数
        /// </summary>
        public int MinMultiple { get; set; }
    }
}

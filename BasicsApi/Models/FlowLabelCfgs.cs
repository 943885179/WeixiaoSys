using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BasicsApi.Models
{
    /// <summary>
    /// 标签配置属性
    /// </summary>
    public class FlowLabelCfgs : WeixiaoEntity
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 标签的属性，标签在元素中的位置 文本相对于节点的位置，目前支持的位置有:  'center'，'top'，'left'，'right'，'bottom'。默认为 'center'。
        /// </summary>
        public string Position { get; set; } = "center";
        public string offsetJson { get; set; }
        /// <summary>
        /// 文本的偏移，在 'top'，'left'，'right'，'bottom' 位置上的偏移量
        /// </summary>
        [NotMapped]
        public int[] Offset
        {
            get { return string.IsNullOrWhiteSpace(this.offsetJson) ? null : this.offsetJson.Split(',').Select(d => Convert.ToInt32(d)).ToArray(); }
            set { this.offsetJson = value == null ? null : string.Join(',', value); }
        }
        /// <summary>
        /// 包裹标签样式属性的字段 style 与标签其他属性在数据结构上并行
        /// </summary>
        public FlowStyle Style { get; set; }
        /// <summary>
        /// 边上的标签文本根据边的方向旋转
        /// </summary>
        public bool AutoRotate { get; set; }
        /// <summary>
        /// 标签文本配置
        /// </summary>
        public virtual FlowLabelCfgs LabelCfg { get; set; }
        /// <summary>
        /// 指定节点周围「上、下、左、右」四个方向上边的连入点LabelCfgs下的只有defaultNode下起作用
        /// </summary>
        public FlowLinkPoints LinkPoints { get; set; }
        /// <summary>
        /// 文本在 X 方向偏移量
        /// </summary>
        public Double RefX { get; set; }
        /// <summary>
        /// 文本在 y 方向偏移量
        /// </summary>
        public Double RefY { get; set; }
    }
}

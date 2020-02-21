using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowNode
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// x
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 文字描述
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// circle圆形：
        ///size 是单个数字，表示直径心位置对应节点的位置
        ///color 字段默认在描边上生效
        ///标签文本默认在节点中央
        ///rect 矩形：
        ///size 是数组，例如：[100, 50],设置单个为正方形
        ///color 字段默认在描边上生效
        ///标签文本默认在节点中央
        ///ellipse 椭圆
        ///size 是数组，表示椭圆的长和宽椭圆的圆心是节点的位置
        ///color 字段默认在描边上生效
        ///标签文本默认在节点中央
        ///diamond 菱形：
        ///size 是数组，表示菱形的长和宽
        ///菱形的中心位置是节点的位置
        ///color 字段默认在描边上生效
        ///标签文本默认在节点中央
        ///triangle 三角形：
        ///size 是数组，表示三角形的长和高
        ///三角形的中心位置是节点的位置
        ///color 字段默认在描边上生效
        ///标签文本默认在节点下方
        ///star 星形：
        ///size 是单个数字，表示星形的大小
        ///星星的中心位置是节点的位置
        ///color 字段默认在描边上生效
        ///标签文本默认在节点中央
        ///image图片：
        ///size 是数组，表示图片的长和宽
        ///图片的中心位置是节点位置
        ///img 图片的路径，也可以在 style 里面设置
        ///color 字段不生效
        ///标签文本默认在节点下方
        ///modelRect菱形：
        ///size 是数组，表示菱形的长和宽
        ///菱形的中心位置是节点的位置
        ///color 字段默认在描边上生效
        ///标签文本默认在节点中央
        ///若有 description 字段则显示在标签文本下方显示 description 内容
        /// </summary>
        public string Shape { get; set; } = "circle";
        /// <summary>
        /// 大小 circle设置半径，矩形椭圆设置长宽[20,10]
        /// </summary>
        public int[] Size { get; set; }
        /// <summary>
        /// anchorPoints 该节点可选的连接点集合
        /// </summary>
        public List<double[][]> AnchorPoints { get; set; }
        /// <summary>
        /// 图片路径，shape设置为image时生效
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 附文本,shape设置modelRect生效
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public LabelCfgs LabelCfg { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public Style Style { get; set; }
    }
    /// <summary>
    /// 标签配置属性
    /// </summary>
    public class LabelCfgs
    {
        /// <summary>
        /// 标签的属性，标签在元素中的位置 文本相对于节点的位置，目前支持的位置有:  'center'，'top'，'left'，'right'，'bottom'。默认为 'center'。
        /// </summary>
        public string Position { get; set; } = "center";
        /// <summary>
        /// 文本的偏移，在 'top'，'left'，'right'，'bottom' 位置上的偏移量
        /// </summary>
        public int[] Offset { get; set; }
        /// <summary>
        /// 包裹标签样式属性的字段 style 与标签其他属性在数据结构上并行
        /// </summary>
        public Style Style { get; set; }
        /// <summary>
        /// 边上的标签文本根据边的方向旋转
        /// </summary>
        public bool AutoRotate { get; set; }
        /// <summary>
        /// 标签文本配置
        /// </summary>
        public LabelCfgs LabelCfg { get; set; }
    }
    public class Style
    {
        /// <summary>
        /// 标签的样式属性，文字字体大小
        /// </summary>
        public int FontSize { get; set; } = 12;
        /// <summary>
        /// 文字样式
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 元素的填充色
        /// </summary>
        public string Fill { get; set; }
        /// <summary>
        /// 元素的描边色
        /// </summary>
        public string Stroke { get; set; }
        /// <summary>
        /// 描边宽度
        /// </summary>
        public int LineWidth { get; set; }
        /// <summary>
        /// 阴影颜色
        /// </summary>
        public string ShadowColor { get; set; }
        /// <summary>
        /// 阴影范围
        /// </summary>
        public string ShadowBlur { get; set; }
        /// <summary>
        /// 阴影 x 方向偏移量
        /// </summary>
        public int ShadowOffsetX { get; set; }
        /// <summary>
        /// 阴影 y 方向偏移量
        /// </summary>
        public int ShadowOffsetY { get; set; }
        /// <summary>
        /// 透明度
        /// </summary>
        public double Opacity { get; set; } = 1;
    }
}

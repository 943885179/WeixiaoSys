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
        /// <summary>
        ///各状态下的样式Object只对keyShape起作用
        /// </summary>
        public List<StateStyle> StateStyles { get; set; }

        /// <summary>
        /// 指定节点周围「上、下、左、右」四个方向上边的连入点
        /// </summary>
        public LinkPoints LinkPoints { get; set; }
        /// <summary>
        /// 图片路径，shape设置为image时生效
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// sharp为imges特有的属性，剪切图片，默认false不开启
        /// </summary>
        public ClipCfg ClipCfg { get; set; }
        /// <summary>
        /// 图标（圆，椭圆，菱形，三角形,五角星,方形卡片）
        /// </summary>

        public Icon Icon { get; set; }
        /// <summary>
        /// 三角形的方向String 可取值：up、down、left、right，默认为up。
        /// </summary>
        public string Direction { get; set; } = "up";
        /// <summary>
        /// 五角星内环大小
        /// </summary>
        public int InnerR { get; set; } = 3 * 8;
    }
    /// <summary>
    /// sharp为imges特有的属性，剪切图片，默认false不开启
    /// </summary>
    public class ClipCfg
    {
        /// <summary>
        /// 剪切的类型，和Shape传入的一样
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 裁剪图形的 x 坐标 类型为 'circle'、'rect'、'ellipse' 时生效
        /// </summary>
        public double X { get; set; } = 0;
        /// <summary>
        /// 裁剪图形的 Y 坐标 类型为 'circle'、'rect'、'ellipse' 时生效
        /// </summary>
        public double Y { get; set; } = 0;
        /// <summary>
        /// 是否启用裁剪功能
        /// </summary>
        public bool Show { get; set; } = false;
        /// <summary>
        /// 剪裁圆形的半径
        /// </summary>
        public double R { get; set; }
        /// <summary>
        /// 剪裁矩形的宽度Number 剪裁 type 为 'rect' 时生效
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// 剪裁矩形的长度Number 剪裁 type 为 'rect' 时生效
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 剪裁椭圆的长轴半径Number剪裁 type 为 'ellipse' 时生效
        /// </summary>
        public double Rx { get; set; }
        /// <summary>
        /// 剪裁椭圆的长轴半径Number 剪裁 type 为 'ellipse' 时生效
        /// </summary>
        public double Ry { get; set; }
    }
    /// <summary>
    /// 标签配置属性
    /// </summary>
    public class LabelCfgs
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Shape { get; set; }
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

        /// <summary>
        /// 指定节点周围「上、下、左、右」四个方向上边的连入点LabelCfgs下的只有defaultNode下起作用
        /// </summary>
        public LinkPoints LinkPoints { get; set; }
        /// <summary>
        /// 文本在 X 方向偏移量
        /// </summary>
        public Double RefX { get; set; }
        /// <summary>
        /// 文本在 y 方向偏移量
        /// </summary>
        public Double RefY { get; set; }
    }
    /// <summary>
    /// 指定节点周围「上、下、左、右」四个方向上边的连入点
    /// </summary>
    public class LinkPoints
    {
        /// <summary>
        /// 是否显示上部的连接点
        /// </summary>
        public bool Top { get; set; } = true;
        /// 是否显示底部的连接点
        /// </summary>
        public bool Bottom { get; set; } = true;
        /// <summary>
        /// 是否显示左部的连接点
        /// </summary>
        public bool Left { get; set; } = true;
        /// <summary>
        /// 是否显示右部的连接点
        /// </summary>
        public bool Right { get; set; } = true;
        /// <summary>
        /// 连接点大小
        /// </summary>
        public double Size { get; set; } = 3;
        /// <summary>
        /// 连接点填充色
        /// </summary>
        public string Fill { get; set; } = "#72CC4A";
        /// <summary>
        /// 连接点的描边颜色
        /// </summary>
        public string Stroke { get; set; } = "#72CC4A";
        /// <summary>
        /// 连接点描边的宽度
        /// </summary>
        public double LineWidth { get; set; } = 1;

    }
    public class Icon
    {
        /// <summary>
        /// 是否显示icon
        /// </summary>
        public bool Show { get; set; } = false;
        /// <summary>
        ///icon的宽度
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        ///icon的高度
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 照片地址
        /// </summary>
        public string Img { get; set; }
    }
    public class Style
    {
        
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
        /// <summary>
        /// Shape为rect时候圆角半径 线拐弯处的圆角弧度
        /// </summary>
        public double Radius { get; set; }
        /// <summary>
        /// 拐弯处距离节点最小距离	Number     默认为 5，polyline 特有
        /// </summary>
        public double Offset { get; set; } = 5;

    }
}

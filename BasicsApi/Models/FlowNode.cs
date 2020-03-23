using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowNode : WeixiaoEntity
    {
        /// <summary>
        /// 组别
        /// </summary>
        public string GroupId { get; set; }
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
        /// circle 圆形：
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
        public FlowLabelCfgs LabelCfg { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public FlowStyle Style { get; set; }
        /// <summary>
        ///各状态下的样式Object只对keyShape起作用
        /// </summary>
        public Dictionary<string, FlowStyle> StateStyles { get; set; }

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
        public FlowClipCfg ClipCfg { get; set; }
        /// <summary>
        /// 图标（圆，椭圆，菱形，三角形,五角星,方形卡片）
        /// </summary>

        public FlowIcon Icon { get; set; }
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
    /// 指定节点周围「上、下、左、右」四个方向上边的连入点
    /// </summary>
    public class LinkPoints : WeixiaoEntity
    {
        /// <summary>
        /// 是否显示上部的连接点
        /// </summary>
        public bool Top { get; set; } = true;
        /// <summary>
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
    public class FlowStyle : WeixiaoEntity
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

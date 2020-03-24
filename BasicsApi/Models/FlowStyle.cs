namespace BasicsApi.Models
{
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

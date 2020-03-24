namespace BasicsApi.Models
{
    /// <summary>
    /// 指定节点周围「上、下、左、右」四个方向上边的连入点
    /// </summary>
    public class FlowLinkPoints : WeixiaoEntity
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
}

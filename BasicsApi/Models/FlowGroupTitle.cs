namespace BasicsApi.Models
{
    /// <summary>
    /// 组文字描述
    /// </summary>
    public class FlowGroupTitle : WeixiaoEntity
    {
        /// <summary>
        /// 文字内容
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 字体边框颜色，同时也支持 fill、fontSize 等所有的文本样式属性
        /// </summary>
        public string Stroke { get; set; }
        /// <summary>
        /// 设置用于填充绘画的颜色、渐变或模式
        /// </summary>
        public string  Fill { get; set; }
        /// <summary>
        /// 文字大小
        /// </summary>
        public string fontSize { get; set; }
        /// <summary>
        /// 可选，默认为 0，表示 x 方向上的偏移量；
        /// </summary>
        public int OffsetX { get; set; } = 0;
        /// <summary>
        ///  可选，默认为 0，表示 Y 方向上的偏移量；
        /// </summary>
        public int OffsetY { get; set; } = 0;
    }
}

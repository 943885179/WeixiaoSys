namespace BasicsApi.Models
{
    public class FlowIcon : WeixiaoEntity
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
}

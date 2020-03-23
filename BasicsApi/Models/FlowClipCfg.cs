namespace BasicsApi.Models
{
    /// <summary>
    /// sharp为imges特有的属性，剪切图片，默认false不开启
    /// </summary>
    public class FlowClipCfg : WeixiaoEntity
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
}

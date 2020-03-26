using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowNode 
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("id")]
        public string Code { get; set; }
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
        public string sizeJson { get; set; }
        /// <summary>
        /// 大小 circle设置半径，矩形椭圆设置长宽[20,10]
        /// </summary>
        [NotMapped]
        public int[] Size
        {
            get { return string.IsNullOrWhiteSpace(this.sizeJson) ? null : this.sizeJson.Split(',').Select(d => Convert.ToInt32(d)).ToArray(); }
            set { this.sizeJson = string.Join(',', value); }
        }
        public string anchorPointsJson { get; set; }
        /// <summary>
        /// anchorPoints 该节点可选的连接点集合
        /// </summary>
        [NotMapped]
        public List<double[][]> AnchorPoints
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.anchorPointsJson) ? null : JsonSerializer.Deserialize<List<double[][]>>(this.anchorPointsJson, options: new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            set
            {
                this.anchorPointsJson = JsonSerializer.Serialize(value, options: new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }
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
        public string stateStylesJson { get; set; }
        /// <summary>
        ///各状态下的样式Object只对keyShape起作用
        /// </summary>
        [NotMapped]
        public Dictionary<string, FlowStyle> StateStyles
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.stateStylesJson) ? null : JsonSerializer.Deserialize<Dictionary<string, FlowStyle>>(this.stateStylesJson, options: new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            set
            {
                this.stateStylesJson = JsonSerializer.Serialize(value, options: new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }
        /// <summary>
        /// 指定节点周围「上、下、左、右」四个方向上边的连入点
        /// </summary>
        public FlowLinkPoints LinkPoints { get; set; }
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
}

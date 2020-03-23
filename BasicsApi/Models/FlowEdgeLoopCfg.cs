namespace BasicsApi.Models
{
    public class FlowEdgeLoopCfg : WeixiaoEntity
    {
        /// <summary>
        /// position: 指定自环与节点的相对位置。默认为：top。支持的值有：top, top-right, right,bottom-right, bottom, bottom-left, left, top-left
        /// </summary>
        public string Position { get; set; } = "top";
        /// <summary>
        /// dist: 从节点 keyShape 的边缘到自环最顶端的位置，用于指定自环的曲度，默认为节点的高度。
        /// </summary>
        public int Dist { get; set; }
        /// <summary>
        /// clockwise: 指定是否顺时针画环，默认为 true。
        /// </summary>
        public bool Clockwise { get; set; } = true;
    }
}

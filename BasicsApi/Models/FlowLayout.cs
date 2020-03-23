namespace BasicsApi.Models
{
    public class FlowLayout : WeixiaoEntity
    {
        /// <summary>
        /// 必须使用小写，大小写敏感
        /// 一般图：
        ///• Random Layout：随机布局；
        ///• Force Layout：经典力导向布局：
        ///力导向布局：一个布局网络中，粒子与粒子之间具有引力和斥力，从初始的随机无序的布局不断演变，逐渐趋于平衡稳定的布局方式称之为力导向布局。适用于描述事物间关系，比如人物关系、计算机网络关系等。
        ///• Circular Layout：环形布局；
        ///• Radial Layout：辐射状布局；
        ///• MDS Layout：高维数据降维算法布局；
        ///• Fruchterman Layout：Fruchterman 布局，一种力导布局；
        ///• Dagre Layout：层次布局。
        ///树图布局：
        ///• Dendrogram Layout：树状布局（叶子节点布局对齐到同一层）；
        ///• CompactBox Layout：紧凑树布局；
        ///• Mindmap Layout：脑图布局；
        ///• Intended Layout：缩进布局。
        /// </summary>
        public string Type { get; set; } = "random";
        /// <summary>
        /// preventOverlap  防止节点重叠
        /// </summary>
        public bool PreventOverlap { get; set; } = true;
        /// <summary>
        /// 指定边距离
        /// </summary>
        public int LinkDistance { get; set; }
    }
}

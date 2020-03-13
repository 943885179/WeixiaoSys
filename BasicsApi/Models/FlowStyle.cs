using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    /// <summary>
    /// 添加样式
    /// </summary>
    public class FlowStyle
    {
        /// <summary>
        /// 添加的样式内容 yarn add insertcss;
        /// </summary>
        public string InsertCss { get; set; } = "/*.g6-tooltip{padding:10px 8px;border:1px solid #e2e2e2;border-radius:4px;background-color:rgba(255,255,255,.9);box-shadow:#aeaeae 0 0 10px;color:#545454;font-size:12px}*/#contextMenu{position:absolute;left:-150px;padding:10px 8px;border:1px solid #e2e2e2;border-radius:4px;background-color:rgba(255,255,255,.9);color:#545454;list-style-type:none;font-size:12px}#contextMenu li{margin-left:0;list-style:none;list-style-type:none;cursor:pointer}#contextMenu li:hover{color:#aaa}";
    }
}

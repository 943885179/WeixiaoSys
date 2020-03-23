using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    /// <summary>
    /// 分组
    /// </summary>
    public class FlowGroup:WeixiaoEntity
    {
        /// <summary>
        /// 组文字描述
        /// </summary>
        public FlowGroupTitle Title { get; set; }
        /// <summary>
        /// 组父Id
        /// </summary>
        public string ParentId { get; set; }
    }
}

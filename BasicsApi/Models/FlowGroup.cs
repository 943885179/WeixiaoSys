using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    /// <summary>
    /// 分组
    /// </summary>
    public class FlowGroup : WeixiaoEntity
    {
        public int TitleId { get; set; }
        /// <summary>
        /// 组文字描述
        /// </summary>
        [ForeignKey("TitleId")]
        public FlowGroupTitle Title { get; set; }
        /// <summary>
        /// 组父Id
        /// </summary>
        public string ParentId { get; set; }
        public int FlowDataId { get; set; }
        [ForeignKey("FlowDataId")]
        public virtual FlowData FlowData { get; set; }
    }
}

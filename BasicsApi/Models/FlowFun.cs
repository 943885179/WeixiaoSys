using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace BasicsApi.Models
{
    /// <summary>
    /// 动态js方法
    /// </summary>
    public class FlowFun : WeixiaoEntity
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunName { get; set; }
        /// <summary>
        /// 传入参数
        /// </summary>
        public string FunParameter { get; set; }
        /// <summary>
        /// 方法体
        /// </summary>
        public string FunBody { get; set; }
        //public int? FlowG6Id { get; set; }
        //[ForeignKey("FlowG6Id")]
        //[JsonIgnore]
        //public FlowG6 FlowG6_FlowFronts { get; }
        ////[ForeignKey("FlowG6Id")]
        //[JsonIgnore]
        //public FlowG6 FlowG6_Ons { get; }
    }
}

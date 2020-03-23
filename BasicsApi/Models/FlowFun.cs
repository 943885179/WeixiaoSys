using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    /// <summary>
    /// 动态js方法
    /// </summary>
    public class FlowFun: WeixiaoEntity
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
    }
}

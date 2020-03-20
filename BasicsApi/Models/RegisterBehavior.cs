using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BasicsApi.Models
{
    public class RegisterBehavior
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Type { get; set; } = "addFlow";
        /// <summary>
        /// 详细内容
        /// </summary>
        public Dictionary<string, FlowFun> Behavior { get; set; }
    }
}

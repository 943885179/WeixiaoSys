using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicsApi.Models
{
    public class FlowRegisterBehavior : WeixiaoEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Type { get; set; } = "addFlow";
        private string behavior { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        [NotMapped]
        public Dictionary<string, FlowFun> Behavior
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.behavior) ? null : JsonSerializer.Deserialize<Dictionary<string, FlowFun>>(this.behavior, options: new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            set
            {
                this.behavior = JsonSerializer.Serialize(value, options: new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }
    }
}

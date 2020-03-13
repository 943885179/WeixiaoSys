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
        public string Type { get; set; } = "Test";
        /// <summary>
        /// 详细内容
        /// </summary>
        public Behavior Behavior { get; set; }
    }
    public class Behavior
    {
        /// <summary>
        /// getevents方法，键值对的值必须是下面的方法名称小写，和前端对应，[后续考虑用反射]
        /// </summary>
        public Dictionary<string, string> GetEvents { get; set; }
        [JsonPropertyName("Fun1")]
        public static FlowFun fun1 { get; set; }

        public FlowFun Fun1
        {
            get { return fun1; }
            set
            {
                var ss = "123";
                if (!string.IsNullOrWhiteSpace(value.FunName))
                {
                    ss = value.FunName;
                }
                var x = new JsonPropertyNameAttribute(ss);
                TypeDescriptor.AddAttributes(typeof(FlowFun),x);
                fun1 = value;
            }
        }

        public FlowFun Fun2 { get; set; }
        public FlowFun Fun3 { get; set; }
        public FlowFun Fun4 { get; set; }
        public FlowFun Fun5 { get; set; }
        public FlowFun Fun6 { get; set; }
        public FlowFun Fun7 { get; set; }
    }
}

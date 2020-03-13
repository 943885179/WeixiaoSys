using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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
        [System.Text.Json.Serialization.JsonPropertyName("Fun1")]
        public static FlowFun fun1 { get; set; }

        public FlowFun Fun1
        {
            get { return fun1; }
            set
            {
                //var ca = TypeDescriptor.GetAttributes(typeof(FlowFun)).OfType<CategoryAttribute>().FirstOrDefault();
                //TypeDescriptor.AddAttributes(typeof(FlowFun), new CategoryAttribute(value.FunName));
                //ca = TypeDescriptor.GetAttributes(typeof(FlowFun))
                //      .OfType<CategoryAttribute>().FirstOrDefault();
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

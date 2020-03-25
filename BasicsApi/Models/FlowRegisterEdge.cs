using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class FlowRegisterEdge : WeixiaoEntity
    {
        public string ShapeType { get; set; } = "line";
        public int ShapeOptionsId { get; set; }
        [ForeignKey("ShapeOptionsId")]
        public FlowShapeOptions ShapeOptions { get; set; } = new FlowShapeOptions();
        public int FlowG6Id { get; set; }
        [ForeignKey("FlowG6Id")]
        public FlowG6 FlowG6 { get; set; }
        // public ExtendShapeType ExtendShapeType { get; set; }
    }
    public class ExtendShapeType
    {
    }
}

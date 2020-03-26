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
        public FlowShapeOptions ShapeOptions { get; set; } = new FlowShapeOptions();
        // public ExtendShapeType ExtendShapeType { get; set; }
    }
    public class ExtendShapeType
    {
    }
}

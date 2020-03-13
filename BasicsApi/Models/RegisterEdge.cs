using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Models
{
    public class RegisterEdge
    {
        public string ShapeType { get; set; } = "line";
        public ShapeOptions ShapeOptions { get; set; } = new ShapeOptions();
        public ExtendShapeType ExtendShapeType { get; set; }
    }

    public class ShapeOptions
    {
        public FlowFun Draw { get; set; } = new FlowFun()
        {
            FunName= "draw",
            FunParameter= "cfg,group",
            FunBody = "return group.addShape('path',{attrs: {path: [['M', cfg.startPoint.x, cfg.startPoint.y],['L', cfg.endPoint.x, cfg.endPoint.y],],stroke: '#333',lineWidth: 3,endArrow: true}});"
        };
    }
    public class ExtendShapeType
    { 
    }
}

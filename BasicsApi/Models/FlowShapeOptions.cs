using System.ComponentModel.DataAnnotations.Schema;

namespace BasicsApi.Models
{
    public class FlowShapeOptions:WeixiaoEntity
    {
        public FlowFun Draw { get; set; } = new FlowFun()
        {
            FunName= "draw",
            FunParameter= "cfg,group",
            FunBody = "return group.addShape('path',{attrs: {path: [['M', cfg.startPoint.x, cfg.startPoint.y],['L', cfg.endPoint.x, cfg.endPoint.y],],stroke: '#333',lineWidth: 2, endArrow: {path: 'M 10,0 L -10,-10 L -10,10 Z',d: 10,},/*endArrow: true*/}});"
        };
    }
}

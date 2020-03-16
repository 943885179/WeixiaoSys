using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : BacsicsController
    {
        // private readonly DepService bll;
        public FlowController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
            // bll = new DepService(db);
        }
        [HttpGet("Test")]
        public ResponseDto Test()
        {
            var flow = new G6ResultDto()
            {
                RegisterBehaviors = new List<RegisterBehavior>() {
                 new RegisterBehavior(){
                     Behavior =new Dictionary<string, FlowFun>(){
                        {"canvas:click",
                             new FlowFun
                             {
                                 FunName="onClickCanvas",
                                 FunParameter="ev",
                                 FunBody="if(this.nodeIndex===undefined){this.nodeIndex=1}this.graph.addItem('node',{x:ev.canvasX,y:ev.canvasY,shape:'rect',id:'node-'+this.nodeIndex,label:'node-'+this.nodeIndex,size:60});this.nodeIndex++;"
                             }
                        },
                        {"node:click",
                             new FlowFun
                             {
                                 FunName="onClickNode",
                                 FunParameter="ev",
                                 FunBody="if(this.addingEdge&&this.edge){this.graph.updateItem(this.edge,{target:ev.item.getModel().id,});this.edge=null;this.addingEdge=false}else{this.edge=this.graph.addItem('edge',{type:'line',source:ev.item.getModel().id,ourceAnchor:true,targetAnchor:true,target:{x:ev.x,y:ev.y},style:{fill:'#333',stroke:'#333',lineWidth:3}});this.addingEdge=true;}"
                             }
                        },
                        {"mousemove",
                             new FlowFun
                             {
                                 FunName="onMousemove",
                                 FunParameter="ev",
                                 FunBody="if(this.addingEdge&&this.edge){this.graph.updateItem(this.edge,{target:{x:ev.x,y:ev.y},})}"
                             }
                        },
                        {"edge:click",
                             new FlowFun
                             {
                                 FunName="onEdgeClick",
                                 FunParameter="ev",
                                 FunBody="const self=this;const currentEdge=ev.item;if(self.addingEdge&&self.edge===currentEdge){self.graph.removeItem(self.edge);self.edge=null;self.addingEdge=false}"
                             }
                        },
                        {"node:contextmenu",
                             new FlowFun
                             {
                                 FunName="nodeContextmenu",
                                 FunParameter="ev",
                                 FunBody="console.log('测试',document.getElementById('contextMenu')); document.getElementById('contextMenu').setAttribute('node',ev.item.getModel().id);ev.preventDefault();ev.stopPropagation();document.getElementById('contextMenu').style.left='${ev.canvasX}px';document.getElementById('contextMenu').style.top='${ev.canvasY}px';"
                             }
                        },
                        {"node:mouseleave",
                             new FlowFun
                             {
                                 FunName="nodeMouseleave",
                                 FunParameter="ev",
                                 FunBody="document.getElementById('contextMenu').removeAttribute('node');document.getElementById('contextMenu').style.left='-150px';"

                             }
                        },
                     }
                 }
                },
                RegisterEdges = new List<RegisterEdge>() {
                 new RegisterEdge(){ }
                },
                FlowStyle = new FlowStyle(),
                FlowData = new FlowData()
                {
                    //      Nodes = new List<FlowNode>()
                    //      {
                    //          new FlowNode() { Id = "node1", Label = "1", Size = new int[] { 80 }, Shape = "rect", Style = new Style() { Fill = "blue" } },
                    //          new FlowNode() { Id = "node2", Label = "2", Size = new int[] { 80, 40 }, Shape = "ellipse" },
                    //          new FlowNode() { Id = "node3", Label = "3", Size = new int[] { 80, 20, 40, 5 }, Shape = "triangle" },
                    //          new FlowNode() { Id = "node4", Label = "4", Size = new int[] { 80 }, LinkPoints = new LinkPoints { } },
                    //          new FlowNode() { Id = "node5", Label = "5", Shape = "star", Style = new Style() { Fill = "red" } },
                    //          new FlowNode() { Id = "node7", Label = "this s", Shape = "modelRect", Description = "谈谈他", Size = new int[] { 40, 50 } },
                    //          new FlowNode() { Id = "node6", Label = "this is image", Img = "https://localhost:5001/upload/1.jpg", Shape = "image", Size = new int[] { 40, 20 } }
                    //      },
                    //      Edges = new List<FlowEdge>
                    //      {
                    //          new FlowEdge()
                    //          {
                    //              Source = "node1",
                    //              Target = "node2",
                    //              Label = "我是描述",
                    //              LabelCfg = new LabelCfgs
                    //              {
                    //                  Position = "end"
                    //              }
                    //          },
                    //          new FlowEdge()
                    //          {
                    //              Source = "node1",
                    //              Target = "node3",
                    //              Label = "dda",
                    //              Shape = "quadratic"
                    //          },
                    //          new FlowEdge()
                    //          {
                    //              Source = "node2",
                    //              Target = "node5",
                    //              Label = "dda"
                    //          },
                    //          new FlowEdge()
                    //          {
                    //              Source = "node2",
                    //              Target = "node4",
                    //              Label = "dda"
                    //          }
                    //      }
                },
                FlowGraph = new FlowGraph()
                {
                    // DefaultNode=new LabelCfgs { LinkPoints=new LinkPoints { } },
                    //layout会让node节点有所偏移，需要进一步解决
                    // Layout = new Layout
                    // {
                    // },
                    Modes = new Mode
                    {
                        // Default=new string[] { "drag-canvas", "drag-node" },
                        Default = new List<object>() {
                           new DragCanvas() ,
                           new DragNode(),
                           new Tooltip(),
                        }
                    },
                    NodeStateStyles = new StateStyle
                    {
                        Hover = new Style
                        {
                            Fill = "red"
                        }
                    }
                },
                Ons = new List<FlowFun>() {
                    new FlowFun
                    {
                        FunName="node:mouseenter",
                        FunParameter="ev",
                        FunBody=new StringBuilder().AppendLine("const nodeItem = ev.item;").AppendLine("this.setItemState(nodeItem, 'hover', true);").ToString()
                    },
                    new FlowFun
                    {
                        FunName="node:mouseleave",
                        FunParameter="ev",
                        FunBody=new StringBuilder().AppendLine("const nodeItem = ev.item;").AppendLine("this.setItemState(nodeItem, 'hover', false);").ToString()
                    }
                }
            };
            result.data = JsonDocument.Parse(JsonSerializer.Serialize(flow, options: new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })).RootElement;
            return result;
        }
    }
}

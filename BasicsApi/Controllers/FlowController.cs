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
using Microsoft.EntityFrameworkCore;

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
            var flow = _db.FlowG6.Include(d => d.FlowCss)
                .Include(d => d.FlowData).ThenInclude(d=>d.Nodes)
                .Include(d => d.FlowData).ThenInclude(d => d.Edges)
                .Include(d => d.FlowData).ThenInclude(d => d.Groups)
                .Include(d => d.FlowFronts)
                .Include(d => d.FlowGraph)
                .Include(d => d.RegisterBehaviors)
                .Include(d => d.RegisterEdges)
                .Include(d => d.FlowGraph.DefaultEdge)
                .Include(d => d.Ons)
                .Include(d => d.FlowGraph.DefaultEdge.LabelCfg)
                .Include(d => d.FlowGraph.DefaultEdge.LabelCfg.LinkPoints)
                .Include(d => d.FlowGraph.DefaultEdge.LabelCfg.LabelCfg)
                .Include(d => d.FlowGraph.DefaultEdge.LinkPoints)
                .Include(d => d.FlowGraph.DefaultNode)
                .Include(d => d.FlowGraph.DefaultNode.LabelCfg)
                .Include(d => d.FlowGraph.DefaultNode.LabelCfg.LinkPoints)
                .Include(d => d.FlowGraph.DefaultNode.LabelCfg.LabelCfg)
                .Include(d => d.FlowGraph.DefaultNode.LinkPoints)
                .FirstOrDefault();
            result.data = JsonDocument.Parse(JsonSerializer.Serialize(flow, options: new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            })).RootElement;
            return result;

        }
        [HttpGet("AddFlowDemo1")]
        public ResponseDto AddFlowDemo1()
        {
            var flow = new FlowG6()
            {
                FlowFronts = new List<FlowFun>()
                {
                    new FlowFun()
                    {
                        FunName = "contextMenu",
                        FunParameter = "item,graph",
                        FunBody = "const conextMenuContainer=document.createElement('ul');conextMenuContainer.id='contextMenu';const edit=document.createElement('li');edit.innerText='编辑';edit.onclick=()=>{ item.oldId=item.id=conextMenuContainer.getAttribute('node');const node=graph.findById(item.id);item.label=node.getModel().label;item.type=node.getModel().shape};conextMenuContainer.appendChild(edit);const removeLi=document.createElement('li');removeLi.innerText='删除';removeLi.onclick=()=>{graph.removeItem(conextMenuContainer.getAttribute('node'));graph.refresh();conextMenuContainer.removeAttribute('node');conextMenuContainer.style.left='-150px'};conextMenuContainer.appendChild(removeLi);const firstLi=document.createElement('li');firstLi.innerText='开始/结束';firstLi.onclick=()=>{graph.updateItem(conextMenuContainer.getAttribute('node'),{shape:'ellipse',size:[60,40]});graph.refreshItem(conextMenuContainer.getAttribute('node'))};conextMenuContainer.appendChild(firstLi);const diamond=document.createElement('li');diamond.innerText='判断';diamond.onclick=()=>{graph.updateItem(conextMenuContainer.getAttribute('node'),{shape:'diamond',size:[60]});graph.refreshItem(conextMenuContainer.getAttribute('node'))};conextMenuContainer.appendChild(diamond);const rect=document.createElement('li');rect.innerText='过程';rect.onclick=()=>{graph.updateItem(conextMenuContainer.getAttribute('node'),{shape:'rect',size:[60]});graph.refreshItem(conextMenuContainer.getAttribute('node'))};conextMenuContainer.appendChild(rect);document.getElementById('mountNode').appendChild(conextMenuContainer);return item;"
                    }, new FlowFun()
                    {
                        FunName = "contextMenu1",
                        FunParameter = "item,graph",
                        FunBody = "const conextMenuContainer=document.createElement('ul');conextMenuContainer.id='contextMenu1';const edit=document.createElement('li');edit.innerText='编辑';edit.onclick=()=>{item.id=conextMenuContainer.getAttribute('node');const node=graph.findById(item.id);item.label=node.getModel().label;item.type=node.getModel().shape};conextMenuContainer.appendChild(edit);const removeLi=document.createElement('li');removeLi.innerText='删除';removeLi.onclick=()=>{graph.removeItem(conextMenuContainer.getAttribute('node'));graph.refresh();conextMenuContainer.removeAttribute('node');conextMenuContainer.style.left='-150px'};conextMenuContainer.appendChild(removeLi);document.getElementById('mountNode').appendChild(conextMenuContainer);return item;"
                    }
                },
                RegisterBehaviors = new List<FlowRegisterBehavior>() {
                    new FlowRegisterBehavior() {
                        Type="addFlow",
                        Behavior = new Dictionary<string, FlowFun>() {
                            { "canvas:click",
                                new FlowFun
                                {
                                    FunName = "onClickCanvas",
                                    FunParameter = "ev",
                                    FunBody = "if(this.nodeIndex===undefined){this.nodeIndex=1}this.graph.addItem('node',{x:ev.canvasX,y:ev.canvasY,shape:'rect',id:'node-'+this.nodeIndex,label:'node-'+this.nodeIndex,size:[60]});this.nodeIndex++;"
                                }
                            },
                            { "node:click",
                                new FlowFun
                                {
                                    FunName = "onClickNode",
                                    FunParameter = "ev",
                                    FunBody = "if(this.addingEdge&&this.edge){this.graph.updateItem(this.edge,{target:ev.item.getModel().id,});this.edge=null;this.addingEdge=false}else{this.edge=this.graph.addItem('edge',{type:'line',source:ev.item.getModel().id,ourceAnchor:true,targetAnchor:true,target:{x:ev.x,y:ev.y},style:{fill:'#333',stroke:'#333',lineWidth:3}});this.addingEdge=true;}"
                                }
                            },
                            { "mousemove",
                                new FlowFun
                                {
                                    FunName = "onMousemove",
                                    FunParameter = "ev",
                                    FunBody = "if(this.addingEdge&&this.edge){this.graph.updateItem(this.edge,{target:{x:ev.x,y:ev.y},})}"
                                }
                            },
                            { "edge:click",
                                new FlowFun
                                {
                                    FunName = "onEdgeClick",
                                    FunParameter = "ev",
                                    FunBody = "const self=this;const currentEdge=ev.item;if(self.addingEdge&&self.edge===currentEdge){self.graph.removeItem(self.edge);self.edge=null;self.addingEdge=false}"
                                }
                            },
                            { "node:contextmenu",
                                new FlowFun
                                {
                                    FunName = "nodeContextmenu",
                                    FunParameter = "ev",
                                    FunBody = "document.getElementById('contextMenu').setAttribute('node',ev.item.getModel().id);ev.preventDefault();ev.stopPropagation();document.getElementById('contextMenu').style.left=`${ev.canvasX}px`;document.getElementById('contextMenu').style.top=`${ev.canvasY}px`;"
                                }
                            },
                            { "node:mouseleave",
                                new FlowFun
                                {
                                    FunName = "nodeMouseleave",
                                    FunParameter = "ev",
                                    FunBody = "document.getElementById('contextMenu').removeAttribute('node');document.getElementById('contextMenu').style.left='-150px';"

                                }
                            },
                            { "edge:contextmenu",
                                new FlowFun
                                {
                                    FunName = "nodeContextmenu",
                                    FunParameter = "ev",
                                    FunBody = "document.getElementById('contextMenu').setAttribute('node',ev.item.getModel().id);ev.preventDefault();ev.stopPropagation();document.getElementById('contextMenu').style.left=`${ev.canvasX}px`;document.getElementById('contextMenu').style.top=`${ev.canvasY}px`;"
                                }
                            },
                            { "edge:mouseleave",
                                new FlowFun
                                {
                                    FunName = "nodeMouseleave",
                                    FunParameter = "ev",
                                    FunBody = "document.getElementById('contextMenu').removeAttribute('node');document.getElementById('contextMenu').style.left='-150px';"

                                }
                            },
                        }
                    }
                },
                RegisterEdges = new List<FlowRegisterEdge>() {
                    new FlowRegisterEdge() { }
                },
                FlowCss = new FlowCss(),
                FlowData = new FlowData()
                {
                    //Nodes = new List<FlowNode>()
                    //      {
                    //          new FlowNode() { Code="node1", Label = "node1", Size = new int[] { 80 }, Shape = "rect", Style = new FlowStyle() { Fill = "blue" } },
                    //          new FlowNode() {Code="node2",Label = "node2", Size = new int[] { 80, 40 }, Shape = "ellipse" },
                    //          new FlowNode() {Code="node3",Label = "node3", Size = new int[] { 80, 20, 40, 5 }, Shape = "triangle" },
                    //          new FlowNode() {Code="node4",Label = "node4", Size = new int[] { 80 }, LinkPoints = new FlowLinkPoints { } },
                    //          new FlowNode() {Code="node5",Label = "node5", Shape = "star", Style = new FlowStyle() { Fill = "red" } },
                    //          new FlowNode() {Code="node6",Label = "this s", Shape = "modelRect", Description = "谈谈他", Size = new int[] { 40, 50 } },
                    //          new FlowNode() {Code="node7",Label = "this is image", Img = "https://localhost:5001/upload/1.jpg", Shape = "image", Size = new int[] { 40, 20 } }
                    //      },
                    //Edges = new List<FlowEdge>
                    //      {
                    //          new FlowEdge()
                    //          {
                    //              Source = "node1",
                    //              Target = "node2",
                    //              Label = "我是描述",
                    //              LabelCfg = new FlowLabelCfgs
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
                    DefaultNode=new FlowLabelCfgs { LinkPoints=new FlowLinkPoints { } },
                   //layout会让node节点有所偏移，需要进一步解决
                    Layout = new FlowLayout
                    {
                    },
                    Modes = new Dictionary<string, object>() {
                        { "default",new List<object>() {
                           new DragCanvas() ,
                           new DragNode(),
                           new Tooltip(),
                        }
                        },
                        { "addFlow",new List<object>() {
                            new OtherModeOption(){ Type="addFlow"},
                           new DragNode(),
                           new ClickSelect()
                        }
                        },
                    },
                    NodeStateStyles = new Dictionary<string, FlowStyle>() {
                        {
                        "hover", new FlowStyle{Fill = "red"}
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
            _db.FlowG6.Add(flow);
            _db.SaveChanges();
            result.data = JsonDocument.Parse(JsonSerializer.Serialize(flow, options: new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })).RootElement;
            return result;
        }
        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="flowData"></param>
        /// <returns></returns>
        [HttpPost("AddFlow")]
        public async Task<ResponseDto> AddFlow(FlowData flowData)
        {
            result.data = JsonSerializer.Serialize(flowData);
            return result;
        }
    }
}

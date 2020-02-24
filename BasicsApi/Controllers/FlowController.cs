using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//using System.Text.Json;
using Newtonsoft.Json;

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
                FlowData = new FlowData()
                {
                    Nodes = new List<FlowNode>()
                    {
                        new FlowNode() { Id = "node1", Label = "1", Size = new int[] { 80 }, Shape = "rect", Style = new Style() { Fill = "blue" } },
                        new FlowNode() { Id = "node2", Label = "2", Size = new int[] { 80, 40 }, Shape = "ellipse" },
                        new FlowNode() { Id = "node3", Label = "3", Size = new int[] { 80, 20, 40, 5 }, Shape = "triangle" },
                        new FlowNode() { Id = "node4", Label = "4", Size = new int[] { 80 },LinkPoints=new LinkPoints{ } },
                        new FlowNode() { Id = "node5", Label = "5", Shape = "star", Style = new Style() { Fill = "red" } },
                        new FlowNode() { Id = "node7", Label = "this s", Shape = "modelRect", Description = "谈谈他", Size = new int[] { 40, 50 } },
                        new FlowNode() { Id = "node6", Label = "this is image", Img = "https://localhost:5001/upload/1.jpg", Shape = "image", Size = new int[] { 40, 20 } }
                    },
                    Edges = new List<FlowEdge>
                    {
                        new FlowEdge()
                        {
                            Source = "node1",
                            Target = "node2",
                            Label = "我是描述",
                            LabelCfg=new LabelCfgs
                            {
                                Position="end"
                            }
                        },
                        new FlowEdge()
                        {
                            Source = "node1",
                            Target = "node3",
                            Label = "dda",
                            Shape="quadratic"
                        },
                        new FlowEdge()
                        {
                            Source = "node2",
                            Target = "node5",
                            Label = "dda"
                        },
                        new FlowEdge()
                        {
                            Source = "node2",
                            Target = "node4",
                            Label = "dda"
                        }
                    }

                },
                FLowGraph = new FLowGraph()
                {
                    Width = 1500,
                    Height = 1000,
                   // DefaultNode=new LabelCfgs { LinkPoints=new LinkPoints { } },
                    Layout = new Layout
                    {
                    },
                    Modes = new Mode
                    {
                        // Default=new string[] { "drag-canvas", "drag-node" },
                        Default = new List<ModeType>(){
                          new ModeType(){
                              Type="drag-canvas"
                          }
                          ,new ModeType(){
                              Type="drag-node",
                              EnableDelegate=true,
                              UpdateEdge=true
                          }
                      }
                    }
                }
            };
            //var jsonStr = JsonConvert.SerializeObject(flow, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //result.data = JsonConvert.DeserializeObject<G6ResultDto>(jsonStr);
            var jsonStr = System.Text.Json.JsonSerializer.Serialize(flow, new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true,
                IgnoreNullValues = true,
            });
            result.data = System.Text.Json.JsonSerializer.Deserialize<G6ResultDto>(jsonStr);
            return result;
        }
    }
}

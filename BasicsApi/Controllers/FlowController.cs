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
        public  ResponseDto Test()
        {
            result.data = new G6ResultDto()
            {
                FlowData = new FlowData()
                {
                    Nodes = new List<FlowNode>()
                    {
                        new FlowNode() { Id = "node1", X = 100, Y = 100, Label = "飒飒" },
                        new FlowNode() { Id = "node2", X = 200, Y = 100, Label = "dd" }
                    },
                    Edges = new List<FlowEdge>
                    {
                        new FlowEdge()
                        {
                            Source="node1",
                            Target="node2",
                            Label="sss"
                        }
                    }

                },
                FLowGraph = new FLowGraph()
                {
                    Width=800,
                    Height=500
                }
            };
            return result;
        }
    }
}

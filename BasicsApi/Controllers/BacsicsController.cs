using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacsicsController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected WeixiaoSysContext _db;
        protected static ResponseDto result;
        public BacsicsController(WeixiaoSysContext db, IMapper mapper)
        {
            result = new ResponseDto();
            if (_db==null)
            {
                lock (this)
                {
                    if (_db == null)
                    {
                        _db = db;
                    }
                }
            }
           if (_mapper==null)
            {
                lock (this)
                {
                    if (_mapper == null)
                    {
                        _mapper = mapper;
                    }
                }
            }
        }
    }
}

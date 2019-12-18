using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BasicsApi.conmon;
using BasicsApi.Dto;
using BasicsApi.Models;
using BasicsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BasicsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacsicsController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected WeixiaoSysContext _db;
        protected static ResponseDto result;
        protected static RsaResponseDto res;
        protected static RSAHelper rsa;
        public BacsicsController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting)
        {
            rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, setting.Value.PrivateKey, setting.Value.PublicKey, setting.Value.AppKey, setting.Value.SplitStr);
            res = new RsaResponseDto();
            result = new ResponseDto();
            if (_db == null)
            {
                lock (this)
                {
                    if (_db == null)
                    {
                        _db = db;
                    }
                }
            }
            if (_mapper == null)
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

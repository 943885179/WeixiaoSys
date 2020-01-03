using System;
using System.Collections.Generic;
using System.IO;
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
    public class UploadController : BacsicsController
    {
        public UploadController(WeixiaoSysContext db, IMapper mapper, IOptions<RSASettings> setting) : base(db, mapper, setting)
        {
        }
        private static string fileRoot = "wwwroot/";//文件根路径
        [HttpPost("Upload")]
        public  ResponseDto Upload(IFormFile file, string fileTypeRoot = "upload") {
            if (file == null || file.Length==0)
            {
                result.status = -1;
                result.msg = "上传文件不能为空";
                return result;
            }
            var type = file.ContentType;
            var fileDir = fileRoot + fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                            Path.GetExtension(file.FileName);
            var filePath = Path.Combine(fileDir, fileName);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            result.data = fileTypeRoot + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + fileName;
            return result;
        }
    }
}

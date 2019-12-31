using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BasicsApi.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace BasicsApi.conmon
{
    public class WeixiaoRequestIMiddleware : IMiddleware
    {
        private readonly ILogger logger;
        private readonly ILoggerHelper log4;
        private RSAHelper rsa;
        private RSASettings setting;
        public WeixiaoRequestIMiddleware(ILogger<WeixiaoRequestIMiddleware> logger, ILoggerHelper log4, IOptions<RSASettings> setting)
        {
            this.logger = logger;
            this.log4 = log4;
            this.setting = setting.Value;
             rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, this.setting.PrivateKey, this.setting.PublicKey, this.setting.AppKey, this.setting.SplitStr);

        }
        //加解密
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
          var originalBodyStream = context.Response.Body;
           try
           {
                context.Request.EnableBuffering();//内存中创建缓冲区存放Request.Body的内容，否则不能使用context.Request.Body.Position;
                //读取请求
                var requestReader = new StreamReader(context.Request.Body);
                var requestContextRsa = await requestReader.ReadToEndAsync();
                context.Request.Body.Position = 0;//重新开始，否则body为空
                var conttype = context.Request.ContentType;
                if (!string.IsNullOrWhiteSpace(requestContextRsa))
                {
                    var rsaDto = JsonConvert.DeserializeObject<RsaDto>(requestContextRsa);
                    var requestContext = rsa.Decrypt(rsaDto.Data);
                    context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestContext));
                    // await stream.CopyToAsync(context.Request.Body);
                }
                using (var ms =new MemoryStream())
                {
                    context.Response.Body = ms;
                    await next(context);
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var responseStr = await new StreamReader(ms).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var path = context.Request.Path.Value;
                    if (!string.IsNullOrWhiteSpace(responseStr) && context.Response.StatusCode==200 )
                    {
                        if (!path.EndsWith(".json"))
                        {
                            var result = new RsaDto()
                            {
                                Data = rsa.AppEncrypt(responseStr)
                            };
                            var array= Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result,Formatting.Indented, new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()}));
                            var newMs = new MemoryStream(array);
                            await newMs.CopyToAsync(originalBodyStream);
                        }else
                        {
                            var array= Encoding.UTF8.GetBytes(responseStr);
                            var newMs = new MemoryStream(array);
                            await newMs.CopyToAsync(originalBodyStream);
                        }
                    }
                }
           }
            catch(WeixiaoException ex){
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        // {
        //     context.Request.EnableBuffering();
        //     var request = context.Request.Body;
        //     var reader=new StreamReader(request);
        //     var reqStr =await reader.ReadToEndAsync();
        //     context.Request.Body.Position = 0;
        //     context.Request.Body=new MemoryStream(Encoding.ASCII.GetBytes(reqStr));
        //     var originalBodyStream = context.Response.Body;
        //     using (var ms=new MemoryStream())
        //     {
        //         context.Response.Body = ms;
        //         await next(context);
        //         context.Response.Body.Seek(0, SeekOrigin.Begin);
        //         var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
        //         context.Response.Body.Seek(0, SeekOrigin.Begin);
        //         byte[] array = Encoding.UTF8.GetBytes("text");
        //         MemoryStream stream = new MemoryStream(array);
        //         await stream.CopyToAsync(originalBodyStream);

        //     }
        // }
    }
    public static class WexiaoRequestIMiddlewareException
    {
        public static IApplicationBuilder UserWeixiaoRequest(this IApplicationBuilder builder){
            return builder.UseMiddleware<WeixiaoRequestIMiddleware>();
        }
    }
}

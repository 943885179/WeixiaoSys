using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BasicsApi.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BasicsApi.conmon
{
    public class WeixiaoErrorIMiddleware : IMiddleware
    {
        private readonly ILogger logger;
        private readonly ILoggerHelper log4;
        private RSAHelper rsa;
        private readonly RSASettings setting;
        public WeixiaoErrorIMiddleware(ILogger<WeixiaoErrorIMiddleware> logger, ILoggerHelper log4, IOptions<RSASettings> setting)
        {
            this.logger = logger;
            this.log4 = log4;
            this.setting = setting.Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var originalBodyStream = context.Response.Body;
            var msg = "";
            try
            {
                await next(context);
            }
            catch (WeixiaoException ex)
            {
                context.Response.StatusCode = 200;
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                log4.Error("系统异常!", ex);
                logger.LogError("系统异常！", ex);
                msg = ex.Message;
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    msg = "请求错误";
                }
                else if (statusCode == 405)
                {
                    msg = "请求方式错误";
                }
                else if (statusCode != 200 && statusCode != 201 && statusCode != 202 && statusCode != 203 && statusCode != 204 && statusCode != 205 && statusCode != 206)
                {
                    msg = "未知错误";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg, originalBodyStream);
                }
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string msg, Stream st)
        {
            rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, setting.PrivateKey, setting.PublicKey, setting.AppKey, setting.SplitStr);
            var result = new RsaDto()
            {
                Data = rsa.AppEncrypt(new ResponseDto() { status = -1, msg = msg })
            };
            var ms = new MemoryStream(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(result, options: new JsonSerializerOptions()
            {
                //IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })));
            await ms.CopyToAsync(st);
            //context.Response.ContentType = "application/json;charset=utf-8";
            //await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            rsa = new RSAHelper(RSAType.RSA2, Encoding.UTF8, setting.PrivateKey, setting.PublicKey, setting.AppKey, setting.SplitStr);
            var result = new RsaDto()
            {
                Data = rsa.AppEncrypt(new ResponseDto() { status = -1, msg = msg })
            };
            /*var str = JsonSerializer.Serialize(result, options: new JsonSerializerOptions()
            {
                //IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });*/
            // var array = Encoding.UTF8.GetBytes(str);
            // var ms = new MemoryStream(array);
            //await ms.CopyToAsync(context.Response.Body);
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
    public static class WeixiaoErrorIMiddlewareExceptions
    {
        public static IApplicationBuilder UseWeixiaoError(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WeixiaoErrorIMiddleware>();
        }
    }
}

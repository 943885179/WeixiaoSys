using System;
using System.Threading.Tasks;
using BasicsApi.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BasicsApi.conmon
{
    public class WeixiaoErrorIMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           var msg = "";
            try
            {
                await next(context);
            }
            catch(WeixiaoException ex){
                context.Response.StatusCode = 200;
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally{
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
               else if (statusCode != 200 && statusCode != 201 && statusCode != 202 && statusCode != 203 && statusCode != 204 && statusCode != 205 && statusCode != 206)
                {
                    msg = "未知错误";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }

           // throw new System.NotImplementedException();
        }
        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
                var result = new ResponseDto() { status=-1,msg=msg };
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
    public static class WeixiaoErrorIMiddlewareExceptions{
        public static IApplicationBuilder UseWeixiaoError(this IApplicationBuilder builder){
            return builder.UseMiddleware<WeixiaoErrorIMiddleware>();
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BasicsApi.conmon
{
    /// <summary>
    ///
    /// </summary>
    public class WeixiaoAsyncAuthorizationFilterAttribute : Attribute,IAsyncAuthorizationFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.Run(() => { Console.WriteLine("权限认证"); });
        }
    }
}

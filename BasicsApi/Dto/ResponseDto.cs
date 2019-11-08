using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class ResponseDto
    {
        /// <summary>
        /// 状态 0：成功， -1：失败
        /// </summary>
        public int status { get; set; } = 0;
        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; } = "成功";
        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }
    }
}

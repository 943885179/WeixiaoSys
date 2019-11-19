using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.conmon
{
    public class WeixiaoException : ApplicationException
    {
        public WeixiaoException() : base("测试异常")
        {

        }
        public WeixiaoException(string message) : base(message)
        {

        }
        public WeixiaoException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

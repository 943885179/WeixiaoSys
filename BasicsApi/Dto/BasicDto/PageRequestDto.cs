using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class PageRequestDto
    {
        public int pi { get; set; }//当前页码
        public int ps { get; set; }//每页数量，当设置为 0 表示不分页，默认：10
    }
}

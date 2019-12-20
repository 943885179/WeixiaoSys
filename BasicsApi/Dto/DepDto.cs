using BasicsApi.Models;

namespace BasicsApi.Dto
{
    public class DepDto:Department
    {
        public int pi { get; set; }//当前页码
        public int ps { get; set; }//每页数量，当设置为 0 表示不分页，默认：10

    }
}

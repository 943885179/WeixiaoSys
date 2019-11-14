namespace BasicsApi.Dto
{
    public class ResultPageDto<T>
    {

        public int total { get; set; }//当前总数据，在服务器渲染时需要传入，默认：0
        public T list { get; set; }//数据
    }
}

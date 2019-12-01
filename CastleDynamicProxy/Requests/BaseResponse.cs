namespace CastleDynamicProxy.Requests
{
    public class BaseResponse<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}

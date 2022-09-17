namespace NewAPI.Services
{
    public class ServiceReturnType<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public T Error { get; set; }
    }
}
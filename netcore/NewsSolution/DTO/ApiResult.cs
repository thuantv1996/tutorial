namespace DTO
{
    public class ApiResult
    {
        public int StatusCode { get; set; }

        public object ResponseData { get; set; }

        public object Error { get; set; }
    }
}

namespace HardCode.WebAPI.Models
{
    public class ResponseModel<T> where T : class
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
    }
}

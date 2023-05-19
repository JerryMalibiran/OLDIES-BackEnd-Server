namespace API.Models
{
    public class ServiceResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ServiceResponse(T? data, int code = 000, string message = "")
        {
            Data = data;
            Code = code;
            Message = message;
        
        }
    }
}

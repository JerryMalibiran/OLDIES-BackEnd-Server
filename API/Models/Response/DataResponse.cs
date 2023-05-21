namespace API.Models.Response
{
    public class DataResponse<T>: IDataResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public DataResponse(T data, int code, string message)
        {
            Data = data;
            Code = code;
            Message = message;

        }
    }
}

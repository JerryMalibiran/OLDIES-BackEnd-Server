namespace API.Models.Response
{
    public class DefaultResponse: IResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public DefaultResponse(int code, string message) 
        {
            Code = code;
            Message = message;
        }    
    }
}

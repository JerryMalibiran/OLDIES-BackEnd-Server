namespace API.Models.Response
{
    public interface IResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}

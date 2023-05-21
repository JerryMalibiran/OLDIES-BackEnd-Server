namespace API.Models.Response
{
    public interface IDataResponse<T>: IResponse
    {
        public T Data { get; set; }
    }
}

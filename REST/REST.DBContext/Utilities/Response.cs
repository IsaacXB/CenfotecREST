namespace REST.Database.Utilities
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Response() { }   

        public Response(T data, string message = null)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}

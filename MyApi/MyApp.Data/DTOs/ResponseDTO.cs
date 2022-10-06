namespace MyApp.Data.DTOs
{
    public class ResponseDTO<T>
    {
        public T Response { get; set; }
        public string Message { get; set; }

        public ResponseDTO(T response, string message)
        {
            Response = response;
            Message = message;
        }

    }
}

namespace BusinessLayer.DTOs
{
    public class ResponseDto<T>
    {
        public string Status { get; set; } // "created", "duplicate", "reactivated"
        public string Message { get; set; }
        public T Data { get; set; }
    }

}

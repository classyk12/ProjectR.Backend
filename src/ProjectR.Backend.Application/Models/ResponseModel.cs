namespace ProjectR.Backend.Application.Models
{
    public class BaseResponseModel
    {
        public bool Status { get; private set; }
        public string? Message { get; private set; }
        public BaseResponseModel(string? message = "", bool status = true)
        {
            Message = message;
            Status = status;
        }
    }
    public class ResponseModel<T> : BaseResponseModel
    {
        public ResponseModel(string? message, T? data, bool status = true) : base(message, status)
        {
            Data = data;
        }

        public T? Data { get; private set; }
    }
}
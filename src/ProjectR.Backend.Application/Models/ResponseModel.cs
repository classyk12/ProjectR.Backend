namespace ProjectR.Backend.Application.Models
{
    public class BaseResponseModel
    {
        public bool Status { get; private set; }
        public string? Message { get; private set; }
        public string[] Errors { get; private set; }
        public BaseResponseModel(string? message = "", bool status = true, string[]? errors = null)
        {
            Message = message;
            Status = status;
            Errors = errors ?? Array.Empty<string>();
        }
    }
    public class ResponseModel<T> : BaseResponseModel
    {
        public ResponseModel(string? message, T? data, bool status = true, string[]? errors = null) : base(message, status, errors)
        {
            Data = data;
        }

        public T? Data { get; private set; }
    }
}
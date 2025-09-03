using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectR.Backend.Application.Models
{
    public class CloudinaryResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? Url { get; set; }
        public string? SecureUrl { get; set; }
        public string? PublicId { get; set; }
        public string? ErrorMessage { get; set; }
        public long? Bytes { get; set; }
        public string? Format { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    

        public static CloudinaryResponseModel Success(string url, string secureUrl, string publicId, long bytes, string format, int width, int height)
        {
            return new CloudinaryResponseModel {
                IsSuccess = true,
                Url = url,
                SecureUrl = secureUrl,
                PublicId = publicId,
                Bytes = bytes,
                Format = format,
                Width = width,
                Height = height 
            };
        }

        public static CloudinaryResponseModel Failure(string errorMessage)
        { 
            return new CloudinaryResponseModel
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;


namespace ProjectR.Backend.Application.Interfaces.Utility
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file, string? folder = null);
        Task<DeletionResult> DeleteResourceAsync(string publicId, ResourceType resourceType = ResourceType.Image);
        string GetOptimizedUrl(string publicId, int? width = null, int? height = null, string format = "auto");
        string GetTransformedImageUrl (string publicId, int? width, int? height, string? effect = null);
    }
}

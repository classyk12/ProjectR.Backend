using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ProjectR.Backend.Application.Interfaces.Utility;


namespace ProjectR.Backend.Infrastructure.Utility
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file, string? folder = null)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required");

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                Transformation = new Transformation()
                                .Quality("auto")
                                .FetchFormat("auto")
            };

            return await _cloudinary.UploadAsync(uploadParams);
        }
        public async Task<DeletionResult> DeleteResourceAsync(string publicId, ResourceType resourceType = ResourceType.Image)
        {
            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = resourceType
            };

            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public string GetOptimizedUrl(string publicId, int? width = null, int? height = null, string format = "auto")
        {
            var transformation = new Transformation()
                .Quality("auto")
                .FetchFormat("format");

            if(width.HasValue) 
                transformation = transformation.Width(width.Value);

            if(height.HasValue)
                transformation = transformation.Height(height.Value);

            return _cloudinary.Api.UrlImgUp.Transform(transformation).BuildUrl(publicId);
        }

        public string GetTransformedImageUrl(string publicId, int? width, int? height, string? effect = null)
        {
            var transformation = new Transformation()
                                        .Width(width)
                                        .Height(height)
                                        .Crop("fill")
                                        .Quality("auto")
                                        .FetchFormat("auto");

            if (!string.IsNullOrEmpty(effect))
            {
                transformation = transformation.Effect(effect);
            }

            return _cloudinary.Api.UrlImgUp.Transform(transformation).BuildUrl(publicId);
        }
    }
}

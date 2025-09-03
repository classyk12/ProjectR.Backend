using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ProjectR.Backend.Application.Interfaces.Utility;
using ProjectR.Backend.Application.Models;
using System.Net;

namespace ProjectR.Backend.Infrastructure.Utility
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<CloudinaryResponseModel> UploadImageAsync(IFormFile file, string? folder = null)
        {
            try
            {

                if (file == null || file.Length == 0)
                {
                    return CloudinaryResponseModel.Failure("File is required");
                }

                if (file.Length > 10 * 1024 * 1024)
                {
                    return CloudinaryResponseModel.Failure("File size cannot exceed 10MB");
                }

                List<string> allowedExtensions = [".jpg", ".jpeg", ".png"];
                string fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    return CloudinaryResponseModel.Failure("Invalid file type, Only JPG, JPEG and PNG Files are allowed");
                }

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

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return CloudinaryResponseModel.Success
                    (
                        result.Url?.ToString() ?? string.Empty,
                        result.SecureUrl.ToString() ?? string.Empty,
                        result.PublicId,
                        result.Bytes,
                        result.Format,
                        result.Width,
                        result.Height
                    );
                }
                else
                {
                    return CloudinaryResponseModel.Failure($"Upload failed with status: {result.StatusCode}");
                }
            }
            catch (Exception ex) 
            {
                return CloudinaryResponseModel.Failure($"Upload failed: {ex.Message}");
            }
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

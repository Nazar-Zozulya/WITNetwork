using System.Buffers.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using WITnetwork.Services;

namespace WITnetwork.Services;

public class PhotoService : IPhotoService
{
    public readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account (
          config.Value.CloudName,
          config.Value.ApiKey,
          config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(string base64)
    {
        var uploadResult = new ImageUploadResult();

        if (!string.IsNullOrEmpty(base64))
            {
                if (!base64.StartsWith("data:"))
            {
                base64 = "data:image/jpeg;base64," + base64; 
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(base64),
                
                Format = "webp" 
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        return await _cloudinary.DestroyAsync(deleteParams);
    }  
}
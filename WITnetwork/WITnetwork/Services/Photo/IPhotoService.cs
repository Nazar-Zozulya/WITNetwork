

using System.Buffers.Text;
using CloudinaryDotNet.Actions;

namespace WITnetwork.Services;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(string base64);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
using Microsoft.AspNetCore.Http;

namespace Personal.Shopping.Services.Product.Application.Interfaces;

public interface IAwsS3BucketService
{
    Task<string> UploadFileAsync(IFormFile file, string fileName);
    Task DeleteFileAsync(string fileName);
}

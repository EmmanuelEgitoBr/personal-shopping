using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Personal.Shopping.Services.Product.Application.Interfaces;

namespace Personal.Shopping.Services.Product.Application.Services;

public class AwsS3BucketService : IAwsS3BucketService
{
    private readonly string _bucketName;
    private readonly IAmazonS3 _s3Client;

    public AwsS3BucketService(IConfiguration config)
    {
        _bucketName = config["AWS:BucketName"]!;

        _s3Client = new AmazonS3Client(
            config["AWS:AccessKey"],
            config["AWS:SecretKey"],
            RegionEndpoint.GetBySystemName(config["AWS:Region"])
        );
    }

    public async Task<string> UploadFileAsync(IFormFile file, string fileName)
    {
        using var stream = file.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.PublicRead
        };

        await _s3Client.PutObjectAsync(putRequest);

        return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);
    }
}

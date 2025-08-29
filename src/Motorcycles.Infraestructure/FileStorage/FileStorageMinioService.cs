using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Motorcycles.Domain.Abstractions.Storage;


namespace Motorcycles.Infraestructure.FileStorage;

public class FileStorageMinioService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private const string BucketName = "cnh-imgs";

    public FileStorageMinioService(string endpoint, string accessKey, string secretKey) 
    {
        _minioClient = new MinioClient()
                        .WithEndpoint(endpoint)
                        .WithCredentials(accessKey, secretKey)
                        .WithSSL(false)
                        .Build();
        _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketName));
    }

    public async Task<string> UploadPhotoCnh(Stream stream, string filename, int size, string contentType)
    {
        // Verificar se o bucket existe, se não, criar
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(BucketName);
        bool bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!bucketExists)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(BucketName);
            await _minioClient.MakeBucketAsync(makeBucketArgs);
        }

        
        var putObjectArgs = new PutObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(filename)
                    .WithStreamData(stream)
                    .WithObjectSize(size)
                    .WithContentType(contentType);

        await _minioClient.PutObjectAsync(putObjectArgs);

        return $"{BucketName}/{filename}";
    }

    public async Task<Stream> GetImageAsync(string objectName)
    {
        var memoryStream = new MemoryStream();

        var getObjectArgs = new GetObjectArgs()
            .WithBucket(BucketName)
            .WithObject(objectName)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream));

        await _minioClient.GetObjectAsync(getObjectArgs);

        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task DeleteAsync(string fileName)
    {
        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(BucketName)
            .WithObject(fileName));
    }

}

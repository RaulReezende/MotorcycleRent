using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Motorcycles.Domain.Abstractions.Storage;


namespace Motorcycles.Infraestructure.FileStorage;

public class FileStorageMinioService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private const string BucketName = "cnh-imgs";
    private const string ContentType = "image/png";

    public FileStorageMinioService(string endpoint, string accessKey, string secretKey) 
    {
        _minioClient = new MinioClient()
                        .WithEndpoint(endpoint)
                        .WithCredentials(accessKey, secretKey)
                        .WithSSL(false)
                        .Build();
        _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketName));
    }

    public async Task<string> UploadPhotoCnh(string cnhImage)
    {

        // Validar tamanho (máximo 5MB para CNH)
        //if (formFile.Length > 5 * 1024 * 1024)
        //    throw new NotFoundException("Imagem muito grande. Tamanho máximo: 5MB");

        // Processar imagem
        string fileName = $"cnh_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString()[..8]}.png";
        if (cnhImage.Contains(','))
            cnhImage = cnhImage.Split(',')[1];

        // Converter base64 para bytes
        var bytes = Convert.FromBase64String(cnhImage);

        using var stream = new MemoryStream(bytes);

        var putObjectArgs = new PutObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(fileName)
                    .WithStreamData(stream)
                    .WithObjectSize(bytes.Length)
                    .WithContentType(ContentType);

        await _minioClient.PutObjectAsync(putObjectArgs);

        return $"{BucketName}/{fileName}";
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

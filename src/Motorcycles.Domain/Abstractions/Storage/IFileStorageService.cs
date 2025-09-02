namespace Motorcycles.Domain.Abstractions.Storage;

public interface IFileStorageService
{
    Task<string> UploadPhotoCnh(string cnhImage);
    Task<Stream> GetImageAsync(string objectName);
}

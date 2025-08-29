using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Abstractions.Storage;

public interface IFileStorageService
{
    Task<string> UploadPhotoCnh(Stream stream, string filename, int size, string contentType);
    Task<Stream> GetImageAsync(string objectName);
}

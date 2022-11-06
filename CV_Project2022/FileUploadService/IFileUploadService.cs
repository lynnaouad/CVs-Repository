using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CV_Project2022.FileUploadService
{
    public interface IFileUploadService
    {
        public Task<string> UploadFileAsync(IFormFile file);
        public IFormFile TransformToIFormFile(string path);
    }
}

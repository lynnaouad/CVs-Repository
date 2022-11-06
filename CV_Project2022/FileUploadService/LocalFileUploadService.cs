using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CV_Project2022.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment environment;
        public string upload_loc = "wwwroot\\images";
        public string image_loc = "~/images/";

        public LocalFileUploadService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }


        public async Task<string> UploadFileAsync(IFormFile file)
        {
            //path where i want to upload the file
            string filePath = Path.Combine(environment.ContentRootPath, upload_loc, file.FileName);

            //upload file
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return filePath;
        }

        public IFormFile TransformToIFormFile(string path)
        {
            IFormFile photo;

            using (var stream = System.IO.File.OpenRead(path))
            {
                photo = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            }

            return photo;
        }

    }
}

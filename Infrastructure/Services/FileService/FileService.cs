using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment) => this.environment = environment;
        public string CreateFile(string filePath, IFormFile file)
        {
            string path = Path.Combine(environment.WebRootPath, filePath, file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return file.FileName;
        }

        public bool DeleteFile(string filePath, string fileName)
        {
            string path = Path.Combine(environment.WebRootPath, filePath,fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}

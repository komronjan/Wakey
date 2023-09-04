using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.FileService
{
    public interface IFileService
    {
        string CreateFile(string filePath, IFormFile file);
        bool DeleteFile(string filePath,string fileName);
    }
}

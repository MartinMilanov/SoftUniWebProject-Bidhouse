using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bidhouse.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment env;

        public FileService(IHostingEnvironment env)
        {
            this.env = env;
        }
        public async Task<string> UploadFile(IFormFileCollection files)
        {
            var file = files[0];

            if (file.Length > 0)
            {
                var uploadDir = @"wwwroot/images";
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var webRootPath = env.ContentRootPath;
                fileName = DateTime.UtcNow
                           .ToString("yyyymmssfff") + fileName + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);

                uploadDir = @"images";
                var dbUrl = "/" + uploadDir + "/" + fileName;


                await file.CopyToAsync(new FileStream(path, FileMode.Create));


                return dbUrl;
            }
            else
            {
                return null;
            }
        }
    }
}


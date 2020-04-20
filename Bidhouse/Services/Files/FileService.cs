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
        public string UploadDir => @"wwwroot/images";
        public FileService(IHostingEnvironment env)
        {
            this.env = env;
        }

        public bool RemoveImage(string imageUrl)
        {
            if (String.IsNullOrWhiteSpace(imageUrl))
            {
                return false;
            }
            if (imageUrl.Contains("default-user-photo"))
            {
                return true;
            }
            var webRootPath = env.ContentRootPath;

            //var path = Path.Combine(webRootPath, @"wwwroot",imageUrl);

            var path = @"wwwroot" + imageUrl;
            File.Delete(path);

            return true;
        }

        public async Task<string> UploadFile(IFormFileCollection files,string previousImageUrl)
        {
            var file = files[0];
            var canProceed = this.RemoveImage(previousImageUrl);

            if (file.Length > 0 && canProceed == true)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var webRootPath = env.ContentRootPath;
                fileName = DateTime.UtcNow
                           .ToString("yyyymmssfff") + fileName + extension;
                var path = Path.Combine(webRootPath, UploadDir, fileName);

                var dbUrl = "/" + "images" + "/" + fileName;


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


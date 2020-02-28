using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse.Services.Files
{
    public interface IFileService
    {
        public Task<string> UploadFile(IFormFileCollection files);
    }
}

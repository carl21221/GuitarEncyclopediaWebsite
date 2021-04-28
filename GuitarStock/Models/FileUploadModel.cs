using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Models
{
    public class FileUploadModel
    {
        public List<IFormFile> Files;

        public string GuitarID;
    }
}

﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DTO_s.Requests
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
        public int ID { get; set; }
    }
}

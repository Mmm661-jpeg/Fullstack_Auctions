using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DomainModels
{
    public class Images
    {
        public int ImageID { get; set; }
        public string ImagePath { get; set; } 
        public DateTime UploadDate { get; set; } 
    }
}

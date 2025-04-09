using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DTO_s.Requests
{
    public class DeleteAuction_DTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int AuctionID { get; set; }
    }
}

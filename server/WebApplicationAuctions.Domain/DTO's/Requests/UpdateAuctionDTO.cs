using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DTO_s.Requests
{
    public class UpdateAuctionDTO
    {
        public int AuctionID { get; set; }
        public string? AuctionName { get; set; }
        public string? AuctionDescription { get; set; }
        public decimal? StartingPrice { get; set; }
        public DateTime? ClosingTime { get; set; }

    }
}

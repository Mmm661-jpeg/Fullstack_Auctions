using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Data.Dto
{
    public class SearchDto
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public string AuctionDescription { get; set; }
        public string AuctionName { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}

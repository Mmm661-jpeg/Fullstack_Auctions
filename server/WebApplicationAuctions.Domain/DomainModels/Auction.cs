using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DomainModels
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public string AuctionName { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public int UserId { get; set; } //foreign key to the user table
        public string AuctionDescription { get; set; }
        public decimal StartingPrice { get; set; }
        public bool IsOpen => ClosingTime > DateTime.Now;

    }
}
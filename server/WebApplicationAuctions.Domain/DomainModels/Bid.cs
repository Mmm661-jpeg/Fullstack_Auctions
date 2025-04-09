using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DomainModels
{
    public class Bid
    {

        public int BidId { get; set; }
        public int BidAmount { get; set; }
        public DateTime? BidTime { get; set; }
        public int UserId { get; set; }
        public int AuctionId { get; set; }


    }
}

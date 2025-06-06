﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAuctions.Domain.DTO_s.Responses
{
    public class SearchAuctionResponse
    {
        public int AuctionId { get; set; }
        public string AuctionName { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public int UserId { get; set; } 
        public string AuctionDescription { get; set; }
        public decimal StartingPrice { get; set; }
        public bool IsOpen => ClosingTime > DateTime.Now;

        public int HighestBidID { get;set; }
        public int HighestBidAmount { get; set; }
        public int HighestBidUserID { get; set; }
    }
}

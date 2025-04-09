using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Data.Interfaces
{
    public interface IAuctionRepo
    {
        public Task<int> CreateAuction(Auction auction);

        public Task<IEnumerable<Auction>> GetAllAuctions();

        public Task<bool> DeleteAuction(int auctionid);

        public Task<int> UpdateAuction(int auctionid, string? auctionname, 
                            string? auctiondescription, DateTime? 
                            closingtime, decimal? startingprice,int userid);

        public Task<IEnumerable<Auction>> GetMyAuctions(int userID);
    }
}

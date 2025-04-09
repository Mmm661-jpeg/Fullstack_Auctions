using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Core.Services
{
    public class BidsService:IBidsService
    {
        private readonly IBidsRepo repo;
        
        public BidsService(IBidsRepo repo)
        {
            this.repo = repo;
        }

        public async Task<bool> MakeBid(int userID, MakeBid_DTO makeBid_DTO)
        {
            var result = await repo.MakeBid(userID, makeBid_DTO.AuctionID, makeBid_DTO.Amount);
            return result;
        }

        public async Task<bool> RemoveBid(int userID, int bidID)
        {
            var result = await repo.RemoveBid(userID, bidID);
            return result;
        }

        public async Task<IEnumerable<Bid>> ViewAllBidsWithID(int userID)
        {
            var result = await repo.ViewAllBidsWithID(userID);
            return result;
        }

        public async Task<IEnumerable<Bid>> ViewBidsOnAuction(int auctionID)
        {
            var result = await repo.ViewBidsOnAuction(auctionID);
            return result;
        }
    }
}

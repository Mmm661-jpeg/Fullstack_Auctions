using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Core.Interfaces
{
    public interface IAuctionService
    {
        public Task<int> CreateAuction(CreateAuctionDto auctionDto, int userId);

        public Task<bool> DeleteAuction(int auctionId);

        public Task<bool> UpdateAuction(UpdateAuctionDTO DTO,int userid);

        public Task<IEnumerable<Auction>> GetAllAuctions();

        public Task<IEnumerable<Auction>> GetMyAuctions(int userId);    

    }
}

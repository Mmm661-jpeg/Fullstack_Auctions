using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DTO_s.Requests;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Core.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepo _repo;

        public AuctionService(IAuctionRepo repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateAuction(CreateAuctionDto auctionDto, int userId)
        {
          
            var auction = new Auction
            {
                AuctionName = auctionDto.AuctionName,
                AuctionDescription = auctionDto.AuctionDescription,
                StartingPrice = auctionDto.StartingPrice,
                OpeningTime = auctionDto.OpeningTime,
                ClosingTime = auctionDto.ClosingTime,
                UserId = userId
            };

            var result = await _repo.CreateAuction(auction);

            return result;
        }

        public async  Task<bool> UpdateAuction(UpdateAuctionDTO DTO,int userid)
        {
            var result = await _repo.UpdateAuction(DTO.AuctionID,DTO.AuctionName,DTO.AuctionDescription,
                DTO.ClosingTime,DTO.StartingPrice,userid);

            return result == 1;
        }

        public async Task<bool> DeleteAuction(int auctionId)
        {
            if(auctionId > 0)
            {
                var result = await _repo.DeleteAuction(auctionId);
                return result;
            }
            else
            {
                return false;
            }
           
        }

        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            try
            {
                var result = await _repo.GetAllAuctions();
                return result ?? Enumerable.Empty<Auction>();
            }
            catch(Exception ex) 
            {
                 Console.Error.WriteLine(ex.Message); //Loggerrr
                return Enumerable.Empty<Auction>();

            }
        }

        public async Task<IEnumerable<Auction>> GetMyAuctions(int userId)
        {
            var result = await _repo.GetMyAuctions(userId);
            return result ?? Enumerable.Empty<Auction>();
        }
    }
}

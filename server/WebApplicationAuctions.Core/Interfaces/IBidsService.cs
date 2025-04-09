using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Core.Interfaces
{
    public interface IBidsService
    {
        Task<bool> MakeBid(int userID, MakeBid_DTO makeBid_DTO);

        Task<bool> RemoveBid(int userID, int bidID);

        Task<IEnumerable<Bid>> ViewAllBidsWithID(int userID);

        Task<IEnumerable<Bid>> ViewBidsOnAuction(int auctionID);
    }
}

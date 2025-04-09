using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Data.Interfaces
{
    public interface IBidsRepo
    {
        Task<bool> MakeBid(int userID, int auctionID, int amount);

        Task<bool> RemoveBid(int userID, int bidID);

        Task<IEnumerable<Bid>> ViewAllBidsWithID(int userID);

        Task<IEnumerable<Bid>> ViewBidsOnAuction(int auctionID);
    }
}

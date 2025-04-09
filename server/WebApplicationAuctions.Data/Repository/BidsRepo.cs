using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Data.Repository
{
    public class BidsRepo : IBidsRepo
    {
        private readonly IAuctionsContext context;
        public BidsRepo(IAuctionsContext context)
        {
            this.context = context;
        }

        public async Task<bool> MakeBid(int userID, int auctionID, int amount)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID);
            parameters.Add("@AuctionID", auctionID);
            parameters.Add("@Amount", amount);

            using(var db = context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QuerySingleAsync<int>("MakeBids2", parameters, commandType: CommandType.StoredProcedure);
                await db.CloseAsync();
                return result == 1;
            }
        }

        public async Task<bool> RemoveBid(int userID, int bidID)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID);
            parameters.Add("@BidID", bidID);

            using(var db = context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QuerySingleAsync<int>("RemoveBid",parameters,commandType: CommandType.StoredProcedure);
                await db.CloseAsync();

                return result == 1;
            }

        }

        public async Task<IEnumerable<Bid>> ViewAllBidsWithID(int userID) //Ienumrable because this result isonly for display directly no need for list or hashset
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID);

            using (var db = context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QueryAsync<Bid>("ViewAllBidsWithID",parameters,commandType: CommandType.StoredProcedure);
                await db.CloseAsync();

                return result;
            }

        }

        public async Task<IEnumerable<Bid>> ViewBidsOnAuction(int auctionID) //List -> IEnumrable : No Need for list operations which makes Ienumrable more effective with low memory usage etc;
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuctionID", auctionID);

            using(var db = context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.QueryAsync<Bid>("ViewBidsOnAuction", parameters, commandType: CommandType.StoredProcedure);
                await db.CloseAsync();

                return result;
            }

        }





    }
}

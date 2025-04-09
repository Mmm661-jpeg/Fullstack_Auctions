using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Identity.Client;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Data.Repository
{
    public class AuctionRepo : IAuctionRepo
    {
        private readonly IAuctionsContext _context;

        public AuctionRepo (IAuctionsContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAuction(Auction auction)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@AuctionName", auction.AuctionName);
            parameters.Add("@AuctionDescription", auction.AuctionDescription);
            parameters.Add("@StartingPrice", auction.StartingPrice);
            parameters.Add("@OpeningTime", auction.OpeningTime);
            parameters.Add("@ClosingTime", auction.ClosingTime);
            parameters.Add("@UserId", auction.UserId);

            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();
                //Execute scalar rather than Querysinglee when we want to pick out 1 result and not column
                var result = await db.ExecuteScalarAsync<int>("CreateAuction2", parameters, commandType: CommandType.StoredProcedure);
                await db.CloseAsync();
                return result;
            }
        }

        
        

        public async Task<bool> DeleteAuction(int auctionid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuctionId",auctionid);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.ExecuteScalarAsync<int>("DeleteAuction",parameters,commandType:CommandType.StoredProcedure);
                await db.CloseAsync();

                return result == 1;


            }
        }

        public async Task<IEnumerable<Auction>> GetAllAuctions() // hash is expensive not the most effective, best used when you need to check contains etc but might aswell use it here
        {
            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QueryAsync<Auction>("GetAllAuctions",commandType:CommandType.StoredProcedure);
                await db.CloseAsync();
                return result; //Pro of using hash could be that it would speed up searches if its all auctions are saved in front and thats used for searches..maybe
            }
        }

        public async Task<int> UpdateAuction(int auctionid,string? auctionname,string? auctiondescription,DateTime? closingtime,decimal? startingprice,int userid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuctionName", auctionname);
            parameters.Add("@ClosingTime", closingtime);
            parameters.Add("@AuctionDescription", auctiondescription);
            parameters.Add("@StartingPrice",startingprice);
            parameters.Add("@AuctionId",auctionid);
            parameters.Add("@UserID", userid);

            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.QuerySingleAsync<int>("UpdateAuction",parameters,commandType:CommandType.StoredProcedure); //This returns a single number executescalar is better for this.
                await db.CloseAsync();
                return result;
               
                    
            }


        }

        public async Task<IEnumerable<Auction>> GetMyAuctions(int userID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.QueryAsync<Auction>("GetMyAuctions", parameters
                    , commandType: CommandType.StoredProcedure);

                await db.CloseAsync();
                return result;
            }
        }
    }
}

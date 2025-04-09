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
    public class ImageRepo:IImageRepo
    {
        private readonly IAuctionsContext _context;

        public ImageRepo(IAuctionsContext context)
        {
            _context = context;
        }

        public async Task<int> AddImage(Images image) //return imageID for later use in service
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Path", image.ImagePath);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.ExecuteScalarAsync<int>("AddImage", parameters,
                   commandType: CommandType.StoredProcedure);

                await db.CloseAsync();

                return result;
            }
        }

        public async Task<Images> GetImage(int imageId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ImageID", imageId);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.QuerySingleAsync<Images>("GetImage",parameters
                    ,commandType: CommandType.StoredProcedure);

                await db.CloseAsync();

                return result;
            }
        }


        
        public async Task<bool> SetAuctionImage(int auctionId, int imageId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuctionID", auctionId);
            parameters.Add("@ImageID",imageId);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.ExecuteScalarAsync<int>("SetAuctionImage", parameters
                    ,commandType: CommandType.StoredProcedure);

                await db.CloseAsync();
                return result == 1;
            }
        }

        public async Task<bool> SetUserProfilePic(int userId, int imageId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userId);
            parameters.Add("@ImageID", imageId);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.ExecuteScalarAsync<int>("SetUserProfilePic", parameters
                    , commandType: CommandType.StoredProcedure);

                await db.CloseAsync();
                return result == 1;
            }
        }

        public async Task<int> GetProfilePic(int userID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.ExecuteScalarAsync<int>("GetProfilePic",parameters,
                    commandType: CommandType.StoredProcedure);

                await db.CloseAsync();
                return result;
            }
        }

        public async Task<int> GetAuctionPic(int auctionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuctionID", auctionID);

            using(var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.ExecuteScalarAsync<int>("GetAuctionPic",parameters,
                    commandType: CommandType.StoredProcedure);

                await db.CloseAsync();
                return result;
            }
        }

        public async Task<bool> DeleteImage(int imageId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ImageID", imageId);

            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.ExecuteScalarAsync<int>("DeleteImg", parameters
                    , commandType: CommandType.StoredProcedure);

                await db.CloseAsync();

                return result == 1;
            }
        }

      
    }
}

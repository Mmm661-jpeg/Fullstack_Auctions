using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Core.Interfaces
{
    public interface IImageService
    {
        //Should get paths etc directly instead of calling id then image etc.
        Task<bool> AddAuctionPic(int auctiondID, IFormFile file); //Add image for auction

        Task<bool> AddUserProfilePic(int UserID, IFormFile file); //Add Image for user

        Task<string> GetProfilePic(int userID); //return path from userpic
        Task<string> GetAuctionPic(int auctionID); //return path form auctionpic

        Task<bool> DeleteAuctionPic(int auctionID); //

        Task<bool> DeleteUserProfilePic(int userID);

    }
}

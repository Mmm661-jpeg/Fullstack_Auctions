using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplicationAuctions.Data.Interfaces
{
    public interface IImageRepo
    {
        Task<int> AddImage(Images image); //Holds information about the image so sets that info
        Task<Images?> GetImage(int imageId); //gets the img entity from the image table

        Task<bool> DeleteImage(int imageId); //Deletes from image table and releted table due to cascade

        Task<bool> SetUserProfilePic(int userId, int imageId); //set relation
        Task<bool> SetAuctionImage(int auctionId, int imageId); //set relation

        Task<int> GetProfilePic(int userID); //gets the imageid from userpic table
        Task<int> GetAuctionPic(int auctionID); //gets the imageid from auctionpic table
    }
}

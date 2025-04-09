using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidsController : ControllerBase
    {
        private readonly IBidsService bidsService;

        public BidsController(IBidsService bidsService)
        {
            this.bidsService = bidsService;
        }

        private int ReadIdFromToken()
        {
            var idClaim = User.FindFirst("UserID");

            if (idClaim != null && int.TryParse(idClaim.Value, out int result))
            {
                return result;
            }
            else
            {
                return -1;
            }


        }

        //Post

        [HttpPost("MakeBid")]
        public async Task<IActionResult> MakeBid([FromBody] MakeBid_DTO makeBid_DTO)
        {


            try
            {
                int userID = ReadIdFromToken();
                if (userID == -1) { return Unauthorized(new { message = "No id found in token" }); }

                var result = await bidsService.MakeBid(userID, makeBid_DTO);


                if (result)
                {
                    return Ok(new { result = true, message = "Succesful bid! good luck!", time = DateTime.Now });
                }
                else
                {
                    return BadRequest(new { result = false, message = "Bid Failed!", time = DateTime.Now });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        //Delete

        [HttpDelete("RemoveBid")]
        public async Task<IActionResult> RemoveBid([FromBody] RemoveBid_DTO removeBid_DTO)
        {

            try
            {

                int userID = ReadIdFromToken();
                if (userID == -1) { return Unauthorized(new { message = "No id found in token" }); }

                var result = await bidsService.RemoveBid(userID, removeBid_DTO.BidID);

                if (result)
                {
                    return Ok(new { result = true, message = "Succefully removed bid!", time = DateTime.Now });
                }
                else
                {
                    return BadRequest(new { result = false, message = "Failed to remove bid", time = DateTime.Now });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }


        }

        //Get

        [HttpGet("ViewAUsersBids")]
        public async Task<IActionResult> ViewAUsersBids([FromQuery] int userId)
        {


            try
            {
                var result = await bidsService.ViewAllBidsWithID(userId);

                if (result == null)
                {
                    return BadRequest(new { message = "No bids found", result = result });
                }
                return Ok(new { message = "Bids found", result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("GetMyBids")]
        public async Task<IActionResult> GetMyBids()
        {


            try
            {
                int userID = ReadIdFromToken();
                if (userID == -1) { return Unauthorized(new { message = "No id found in token" }); }

                var result = await bidsService.ViewAllBidsWithID(userID);

                if (result == null)
                {
                    return BadRequest(new { message = "No bids found", result = result });
                }
                return Ok(new { message = "Found Bids", result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });


            }
        }

            [AllowAnonymous]
            [HttpGet("ViewBidsOnAuction")]
            public async Task<IActionResult> ViewBidsOnAuction([FromQuery] int auctionID) //Get so fromQuery
            {


                try
                {
                    var result = await bidsService.ViewBidsOnAuction(auctionID);
                    if (result == null)
                    {
                        return BadRequest(new { message = "No bids found", result = result });
                    }
                    else
                    {
                        return Ok(new { message = "Bids found", result = result });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
                }
            }
    }
}   

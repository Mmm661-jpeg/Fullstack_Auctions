using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _service;

        public AuctionController(IAuctionService service)
        {
            _service = service;
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

        [HttpPost("CreateAuction")]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionDto auctionDto)
        {
            try
            {


                int userid = ReadIdFromToken();

                var result = await _service.CreateAuction(auctionDto, userid);

                if (result == 0)
                {
                    return Ok(new { message = "Failed to create auction", result = result });
                }
                else
                {
                    return Ok(new { message = "Auction created successfully you can find your auction id in result ", result = result });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }


        [HttpDelete("DeleteAuction")]
        public async Task<IActionResult> DeleteAuction([FromBody] DeleteAuction_DTO deleteAuction_DTO)
        {

            try
            {
                var result = await _service.DeleteAuction(deleteAuction_DTO.AuctionID);

                if (result)
                {
                    return Ok(new { result = result, message = "Auction deleted" });
                }


                else
                {
                    return BadRequest(new { result, message = "Failed to delete auction" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("UpdateAuction")]
        public async Task<IActionResult> UpdateAuction([FromBody] UpdateAuctionDTO auctionDto)
        {


            try
            {
                int userID = ReadIdFromToken();
                if (userID == -1) { return Unauthorized(new { message = "No id found in token" }); }

                var result = await _service.UpdateAuction(auctionDto, userID);
                if (result)
                {
                    return Ok(new { result = true, message = "Auction updated!" });
                }
                else
                {
                    return BadRequest(new { result = false, message = "Auction not updated!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("GetMyAuctions")]
        public async Task<IActionResult> GetMyAuctions()
        {
            try
            {
                int userID = ReadIdFromToken();
                if (userID == -1) { return Unauthorized(new { message = "No id found in token" }); }

                var result = await _service.GetMyAuctions(userID);

                if (result.Any())
                {
                    return Ok(new { result = result, message = "My Auctions found!" });
                }
                else
                {
                    return NotFound(new { result = result, message = "My Auctions not found.." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAllAuctions")]
        public async Task<IActionResult> GetAllAuctions()
        {


            try
            {
                var result = await _service.GetAllAuctions();
                if (result.Any())
                {
                    return Ok(new { result = result, message = "All Auctions found!" });
                }
                else
                {
                    return NotFound(new { result = result, message = "All the Auctions not found.." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }



    }
}

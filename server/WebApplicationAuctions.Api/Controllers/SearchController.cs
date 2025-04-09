using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Core.Services;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("SearchAuctionsKeyword")]
        public async Task<IActionResult> SearchAuctions([FromQuery] string keyword) //get so froQuery is fine
        {
           

            try
            {
                var result = await _searchService.SearchAuctions(keyword);

                return Ok(result); //if-else?

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }


        [HttpGet("SearchAuctionById")]
        public async Task<IActionResult> SearchAuctionById([FromQuery] int auctionID)
        {
            try
            {
                var results =  await _searchService.SearchAuctionById(auctionID);

                if (results.AuctionId == 0)
                {
                    return BadRequest($"No auction found with ID: {auctionID}.");
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}

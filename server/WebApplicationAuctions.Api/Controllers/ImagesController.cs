using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Core.Services;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImagesController(IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            this.imageService = imageService;
            this.webHostEnvironment = webHostEnvironment;
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

        [HttpPost("test-upload")]
        [Consumes("multipart/form-data")]
        public IActionResult TestUpload([FromForm] FileUploadModel model)
        {
            return Ok(new
            {
                model.ID,
                fileName = model.File.FileName,
                size = model.File.Length,
            });
        }

        //[Authorize]
        [HttpPost("AddAuctionPic")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddAuctionPic([FromForm] FileUploadModel model)

        {
            try
            {
                if(model.File == null || model.File.Length == 0)
                {
                    return BadRequest(new { message = "No file provided", result = false });
                }

                var result = await imageService.AddAuctionPic(model.ID, model.File);
                if(result)
                {
                    return Ok(new {message="Succesfull uppload",result=true});
                }
                else
                {
                    return BadRequest(new { message = "Upload failed", result = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        //[Authorize]
        [HttpPost("AddUserProfilePic")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddUserProfilePic([FromForm] FileUploadModel model)
        {
           

            try
            {
                var userid = ReadIdFromToken();
                if (userid == -1) { return Unauthorized(new { message = "No id found in token" }); }

                model.ID = userid; //alwas default userid to loggedinuserID

                if (model.File == null || model.File.Length == 0)
                {
                    return BadRequest(new { message = "No file provided", result = false });
                }

                var result = await imageService.AddUserProfilePic(model.ID, model.File);
                if (result)
                {
                    return Ok(new { message = "Succesfull uppload", result = true });
                }
                else
                {
                    return BadRequest(new { message = "Upload failed", result = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [Authorize]
        [HttpDelete("DeleteAuctionPic")]
        public async Task<IActionResult> DeleteAuctionPic([FromBody] int auctionID)
        {
            try
            {
                var result = await imageService.DeleteAuctionPic(auctionID);

                if(result)
                {
                    return Ok(new { message = "Succesfull delete", result = true });
                }
                else
                {
                    return BadRequest(new { message = "Delete failed", result = false });
                }
            }
            catch
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [Authorize]
        [HttpDelete("DeleteUserProfilePic")]
        public async Task<IActionResult> DeleteUserProfilePic([FromBody] int userID)
        {
            try
            {
                var result = await imageService.DeleteUserProfilePic(userID);

                if (result)
                {
                    return Ok(new { message = "Succesfull delete", result = true });
                }
                else
                {
                    return BadRequest(new { message = "Delete failed", result = false });
                }
            }
            catch
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("GetAuctionPic")]
        public async Task<IActionResult> GetAuctionPic([FromQuery] int auctionID)
        {
            try
            {
                var picPath = await imageService.GetAuctionPic(auctionID);

                if (string.IsNullOrEmpty(picPath))
                {
                    return NotFound(new { message = "Image not found", result = false });
                }

                var fullPath = Path.Combine(webHostEnvironment.WebRootPath, picPath.TrimStart('/'));

                if (!System.IO.File.Exists(fullPath))
                    return NotFound(new { message = "Image file not found", result = false });

                var contentType = GetContentType(fullPath);

                return PhysicalFile(fullPath, contentType);


            }
            catch
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("GetProfilePic")]
        public async Task<IActionResult> GetProfilePic([FromQuery] int userID)
        {
            try
            {
                var picPath = await imageService.GetProfilePic(userID);

                if (string.IsNullOrEmpty(picPath))
                {
                    return NotFound(new { message = "Image not found", result = false });
                }

                var fullPath = Path.Combine(webHostEnvironment.WebRootPath, picPath.TrimStart('/'));

                if (!System.IO.File.Exists(fullPath))
                    return NotFound(new { message = "Image file not found", result = false });

                var contentType = GetContentType(fullPath);

                return PhysicalFile(fullPath, contentType);


            }
            catch
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }


    }
}

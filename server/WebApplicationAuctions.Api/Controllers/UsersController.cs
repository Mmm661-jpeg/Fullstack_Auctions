using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Core.Services;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace WebApplicationAuctions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IUsersService service;

        public UsersController(IUsersService usersService) 
        {
            
            service = usersService;
        }

      


        //POST:

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] Login_Register_DTO users)
        {
            try
            {
                bool result = await service.RegisterUser(users);

                if (result)
                {
                    return Ok(new { result = result, message = "User registered successfully" });
                }
                else
                {
                    return BadRequest(new { result = result, message = "Register failed" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        [HttpPost("Login")]
        public async Task <IActionResult> Login([FromBody] Login_Register_DTO users)
        {
           
            try
            {
                var result = await service.Login(users);

                if (result == null)
                {
                    return Unauthorized(new { message = "Invalid username or password." });

                }
                else
                {
                    return Ok(new { token = result });
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        //Get:
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await service.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [Authorize]
        [HttpGet("GetUserWithID")] //Can be used to search users and also to get userinfo back in refresh
        public async Task<IActionResult> GetUserWithID(int id)
        {
            
            try
            {
                var result = await service.GetUserWithID(id);

                if (result != null)
                {

                    return Ok(new { result = result, message = "Get User with ID successfull" });
                }
                else
                {
                    return BadRequest((new { result = result, message = "Get User with ID successfull" }));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }


        }

        [Authorize]
        [HttpGet("GetUserWithJwt")] //This can be used to read token sent in header to get username and id alt to GetUserWithID
        public IActionResult GetUserWithJwt()
        {
          
            try
            {
                int id = ReadIdFromToken();
                string username = User.FindFirst("Username").Value;

                if (string.IsNullOrEmpty(username) || id == -1)
                {
                    return BadRequest(new { result = false, message = "Get User info with token failed" });
                }

                else
                {
                    Users user = new Users()
                    {
                        UserId = id,
                        UserName = username,
                    };

                    return Ok(new { result = user, message = "Get User with ID successfull" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        //PUT:

        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Updateusers([FromBody] UpdateUser_DTO users)
        {
           
            try
            {
                var id = ReadIdFromToken();
                if (id == -1) { return Unauthorized(new { message = "No id found in token" }); }

                bool result = await service.UpdateUsers(users, id);

                if (result)
                {
                    return Ok(new { result = result, message = "Update successfull" });
                }
                else
                {
                    return BadRequest(new { result = result, message = "Update failed" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

    
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery]int id)

        {
          
            try
            {
                var result = await service.DeleteUser(id);

                if (result)
                {
                    return Ok(new { result = result, message = "Delete succesfull" });
                }
                else
                {
                    return BadRequest(new { result = result, message = "Delete failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
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


    }
}

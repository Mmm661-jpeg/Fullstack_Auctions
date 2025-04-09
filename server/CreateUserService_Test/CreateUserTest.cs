using Azure.Identity;
using Moq;
using Xunit;
using WebApplicationAuctions.Core.Services;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Data.Repository;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Core.Interfaces;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using WebApplicationAuctions.Domain.DTO_s.Requests;

namespace CreateUserService_Test /////////Not comlete!
{
    public class CreateUserTest 
    {
        //Test för att registrera en användare

        [Fact]
        public void RegisterUser_ShouldReturnSuccess_WhenUserIsCreated()
        {
            //arrange
            var mockUserRepo = new Mock<IUserRepo>();
            var mockJwtGenerator = new Mock<IJwtGenerator>();
            var mockLogger = new Mock<ILogger<UsersService>>();

            var userService = new UsersService(mockUserRepo.Object,mockLogger.Object,mockJwtGenerator.Object);

            var username = "ThetestuserforTest";
            var password = "testpassword";

            var expectedResult = true;


            var expectedRequest = new Login_Register_DTO
            {
                Username = username,
                Password = password

            };

            mockUserRepo
                .Setup(repo => repo.RegisterUser(expectedRequest.Username,expectedRequest.Password))
                    .Returns(expectedResult); //här mockas repometoden

            //act
            var result = userService.RegisterUser(expectedRequest);

            //assert
            Assert.NotNull(result);
            Assert.True(result);
            mockUserRepo.Verify(repo => repo.RegisterUser(expectedRequest.Username, expectedRequest.Password),
                Times.Once);
        }
    }
}
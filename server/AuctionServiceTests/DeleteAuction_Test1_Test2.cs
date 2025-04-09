using Moq;
using WebApplicationAuctions.Core.Services;
using WebApplicationAuctions.Data.Interfaces;

namespace AuctionServiceTests
{
    public class AuctionServiceTests
    {
        [Fact] //Test 1 f�r att kontrollera att det lyckas med att ta bort en auktion om det inte finns n�gra bud
        public void DeleteAuction_ShouldSucceed_WhenNoBidsExist()
        {
            //arrange
            var mockRepo = new Mock<IAuctionRepo>();
            var auctionService = new AuctionService(mockRepo.Object);

            int auctionId = 1;

            //mockar repometoden f�r att simulera att auktionen kan tas bort om den ej har n�gra bud
            mockRepo.Setup(repo => repo.DeleteAuction(auctionId))
                .Returns(true);

            //act
            var result =  auctionService.DeleteAuction(auctionId);

            //assert
            Assert.True(result == true);
            Assert.NotNull(result);
            mockRepo.Verify(repo => repo.DeleteAuction(auctionId), Times.Once);
        }

        //test 2 f�r att kontrollera att en auktion EJ kan tas bort om det FINNS bud

        [Fact]
        public void DeleteAuction_ShouldFail_WhenBidsExist()
        {
            //arrange
            var mockRepo = new Mock<IAuctionRepo>();
            var auctionService = new AuctionService(mockRepo.Object);

            int auctionId = 2;

            //mocka repometoden f�r att simulera att auktionen inte kan tas bort om det finns bud p� den

            mockRepo.Setup(repo => repo.DeleteAuction(auctionId))
                .Returns(false);

            //act
            var result =  auctionService.DeleteAuction(auctionId);
            Assert.False(result == true); //kontrollerar att operationen misslyckas
            Assert.NotNull(result);
            mockRepo.Verify(repo => repo.DeleteAuction(auctionId), Times.Once);

        }
    }
}
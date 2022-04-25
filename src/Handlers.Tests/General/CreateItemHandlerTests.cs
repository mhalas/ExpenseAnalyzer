using Api.HandlerRequests;
using DataAccess.Contracts.Model;
using DataAccess.EntityFramework;
using Handlers.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Tests.Generals
{
    [TestFixture]
    public class CreateItemHandlerTests
    {
        private ExpenseDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ExpenseDbContext>()
                .UseInMemoryDatabase("fakedb")
                .Options;

            _dbContext = Substitute.For<ExpenseDbContext>(options);
        }

        private CreateItemHandler CreateCreateItemHandler()
        {
            return new CreateItemHandler(_dbContext);
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            CreateItemRequest request = new CreateItemRequest(new User());
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            _dbContext
                .AddAsync((object)request.NewObject, cancellationToken)
                .ReturnsForAnyArgs(new ValueTask<EntityEntry>());
            var createItemHandler = this.CreateCreateItemHandler();

            // Act
            var result = await createItemHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.AreEqual(5, result.Item.Id);
            Assert.AreEqual("Success", result.Message);
            Assert.AreEqual(true, result.IsSuccess);
        }
    }
}

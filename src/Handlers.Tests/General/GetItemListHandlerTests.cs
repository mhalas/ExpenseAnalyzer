using Api.HandlerRequests;
using DataAccess.EntityFramework;
using Handlers;
using Handlers.Tests.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Tests.Generals
{
    [TestFixture]
    public class GetItemListHandlerTests
    {
        private ExpenseDbContext subExpenseDbContext;

        [SetUp]
        public void SetUp()
        {
            this.subExpenseDbContext = Substitute.For<ExpenseDbContext>();
        }

        private GetItemListHandler<GeneralTestModel> CreateGetItemListHandler()
        {
            return new GetItemListHandler<GeneralTestModel>(
                this.subExpenseDbContext);
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var getItemListHandler = this.CreateGetItemListHandler();
            GetItemsRequest<GeneralTestModel> request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await getItemListHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.Fail();
        }
    }
}

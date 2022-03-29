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
    public class RemoveItemHandlerTests
    {
        private ExpenseDbContext subExpenseDbContext;

        [SetUp]
        public void SetUp()
        {
            this.subExpenseDbContext = Substitute.For<ExpenseDbContext>();
        }

        private RemoveItemHandler<GeneralTestModel> CreateRemoveItemHandler()
        {
            return new RemoveItemHandler<GeneralTestModel>(
                this.subExpenseDbContext);
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var removeItemHandler = this.CreateRemoveItemHandler();
            RemoveItemRequest<GeneralTestModel> request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await removeItemHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.Fail();
        }
    }
}

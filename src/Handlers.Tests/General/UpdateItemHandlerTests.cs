using Api.HandlerRequests;
using AutoMapper;
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
    public class UpdateItemHandlerTests
    {
        private ExpenseDbContext subExpenseDbContext;
        private IMapper subMapper;

        [SetUp]
        public void SetUp()
        {
            this.subExpenseDbContext = Substitute.For<ExpenseDbContext>();
            this.subMapper = Substitute.For<IMapper>();
        }

        private UpdateItemHandler<UserReferenceTestModel> CreateUpdateItemHandler()
        {
            return new UpdateItemHandler<UserReferenceTestModel>(
                this.subExpenseDbContext,
                this.subMapper);
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var updateItemHandler = this.CreateUpdateItemHandler();
            UpdateItemRequest request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await updateItemHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.Fail();
        }
    }
}

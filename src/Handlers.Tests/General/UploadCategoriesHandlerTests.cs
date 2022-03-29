using Api.HandlerRequests;
using DataAccess.EntityFramework;
using Handlers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Tests.Generals
{
    [TestFixture]
    public class UploadCategoriesHandlerTests
    {
        private ExpenseDbContext subExpenseDbContext;

        [SetUp]
        public void SetUp()
        {
            this.subExpenseDbContext = Substitute.For<ExpenseDbContext>();
        }

        private UploadCategoriesHandler CreateUploadCategoriesHandler()
        {
            return new UploadCategoriesHandler(
                this.subExpenseDbContext);
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var uploadCategoriesHandler = this.CreateUploadCategoriesHandler();
            UploadCategoriesRequest request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await uploadCategoriesHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.Fail();
        }
    }
}

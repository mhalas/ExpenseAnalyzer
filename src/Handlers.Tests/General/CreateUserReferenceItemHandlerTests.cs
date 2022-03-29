using Api.HandlerRequests;
using AutoMapper;
using Handlers;
using Handlers.Tests.Models;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Tests.Generals
{
    [TestFixture]
    public class CreateUserReferenceItemHandlerTests
    {
        private IMediator subMediator;
        private IMapper subMapper;

        [SetUp]
        public void SetUp()
        {
            this.subMediator = Substitute.For<IMediator>();
            this.subMapper = Substitute.For<IMapper>();
        }

        private CreateUserReferenceItemHandler<UserReferenceTestModel> CreateCreateUserReferenceItemHandler()
        {
            return new CreateUserReferenceItemHandler<UserReferenceTestModel>(
                this.subMediator,
                this.subMapper);
        }

        [Test]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var createUserReferenceItemHandler = this.CreateCreateUserReferenceItemHandler();
            CreateUserReferenceItemRequest request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await createUserReferenceItemHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.Fail();
        }
    }
}

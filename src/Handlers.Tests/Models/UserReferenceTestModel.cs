using DataAccess.Contracts.Model;
using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;

namespace Handlers.Tests.Models
{
    internal class UserReferenceTestModel : IId, IUserReference
    {
        public int UserId { get; set; }
        public User User { get; }
        public int Id { get; set; }
    }
}

using DataAccess.Contracts.Model;

namespace DataAccess.Contracts.References
{
    public interface IUserReference
    {
        int UserId { get; set; }
        User User { get; }
    }
}

using DataAccess.Contracts.Model;

namespace DataAccess.Contracts.References
{
    public interface IUserCategoryReference
    {
        int UserCategoryId { get; set; }
        UserCategory UserCategory { get; set; }
    }
}

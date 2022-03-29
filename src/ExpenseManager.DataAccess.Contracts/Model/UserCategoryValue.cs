using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using System.Text.Json.Serialization;

namespace DataAccess.Contracts.Model
{
    public partial class UserCategoryValue: IId, IUserCategoryReference
    {
        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string SellerName { get; set; }
        [JsonInclude]
        public int UserCategoryId { get; set; }

        [JsonIgnore]
        public UserCategory UserCategory { get; set; }
    }
}

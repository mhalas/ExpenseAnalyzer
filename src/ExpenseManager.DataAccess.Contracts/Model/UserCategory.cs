using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Contracts.Model
{
    public partial class UserCategory: IId, IUserReference
    {
        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string CategoryName { get; set; }
        [JsonInclude]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public ICollection<Expense> Expenses { get; set; }
        [JsonIgnore]
        public ICollection<UserCategoryValue> UserCategoryValues { get; set; }
    }
}

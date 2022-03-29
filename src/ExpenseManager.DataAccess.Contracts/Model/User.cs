using DataAccess.Contracts.Shared;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Contracts.Model
{
    public partial class User: IId
    {
        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string UserName { get; set; }
        [JsonInclude]
        public string Password { get; set; }

        [JsonIgnore]
        public ICollection<Expense> Expenses { get; set; }
        [JsonIgnore]
        public ICollection<UserCategory> UserCategories { get; set; }
        [JsonIgnore]
        public ICollection<UserConfiguration> UserConfigurations { get; set; }

    }
}

using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using System.Text.Json.Serialization;

namespace DataAccess.Contracts.Model
{
    public partial class UserConfiguration: IId, IUserReference
    {
        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string Key { get; set; }
        [JsonInclude]
        public string Value { get; set; }
        [JsonInclude]
        public string Description { get; set; }
        [JsonInclude]
        public int UserId { get; set; }
        
        [JsonIgnore]
        public User User { get; set; }
    }
}

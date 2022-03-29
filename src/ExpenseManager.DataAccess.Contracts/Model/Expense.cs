using DataAccess.Contracts.References;
using DataAccess.Contracts.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Contracts.Model
{
    public partial class Expense: IId, IUserReference, IUserCategoryReference
    {
        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string Description { get; set; }
        [JsonInclude]
        public DateTime PayDate { get; set; }
        [JsonInclude]
        public decimal Price { get; set; }
        [JsonInclude]
        public string SellerName { get; set; }
        [JsonInclude]
        public int UserId { get; set; }
        [JsonInclude]
        public int UserCategoryId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public UserCategory UserCategory { get; set; }
    }
}

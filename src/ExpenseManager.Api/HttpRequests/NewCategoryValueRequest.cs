using System.ComponentModel.DataAnnotations;

namespace Api.HttpRequests
{
    public class NewCategoryValueRequest
    {
        [Required]
        public string Value { get; set; }
    }
}

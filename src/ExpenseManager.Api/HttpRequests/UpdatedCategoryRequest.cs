using System.ComponentModel.DataAnnotations;

namespace Api.HttpRequests
{
    public class UpdatedCategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}

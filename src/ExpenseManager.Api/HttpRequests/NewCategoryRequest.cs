using System.ComponentModel.DataAnnotations;

namespace Api.HttpRequests
{
    public class NewCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}

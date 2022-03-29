using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.HttpRequests
{
    public class UploadCategoriesRequest
    {
        [Required]
        public Dictionary<string,List<string>> CategoriesDictionary { get; set; }
    }
}

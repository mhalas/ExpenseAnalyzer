using System.ComponentModel.DataAnnotations;

namespace Api.HttpRequests
{
    public class NewUserConfigurationRequest
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }

        public string Description { get; set; }
    }
}

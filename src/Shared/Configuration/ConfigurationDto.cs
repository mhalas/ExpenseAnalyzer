using System.Collections.Generic;

namespace Shared.Configuration
{
    public class ConfigurationDto
    {
        public ConfigurationDto(string defaultCategoryName,
            IDictionary<string, List<ConfigurationColumnDto>> columnDefinitions,
            IDictionary<string, List<string>> categoryDictionary)
        {
            CategoryDictionary = categoryDictionary;
            DefaultCategoryName = defaultCategoryName;
            ColumnDefinitions = columnDefinitions;
        }
        public string DefaultCategoryName { get; set; }
        public IDictionary<string, List<ConfigurationColumnDto>> ColumnDefinitions { get; }
        public IDictionary<string, List<string>> CategoryDictionary { get; }
    }
}

using System.Collections.Generic;

namespace Shared.Configuration
{
    public class ConfigurationDto
    {
        public ConfigurationDto(IDictionary<string, List<ConfigurationCategoryFilterDto>> categoryDictionary,
            string defaultCategoryName, 
            ConfigurationColumnDto valueDateColumn, 
            ConfigurationColumnDto amountColumn, 
            ConfigurationColumnDto titleColumn, 
            ConfigurationColumnDto sourceColumn)
        {
            CategoryDictionary = categoryDictionary;
            DefaultCategoryName = defaultCategoryName;
            ValueDateColumn = valueDateColumn;
            AmountColumn = amountColumn;
            TitleColumn = titleColumn;
            SourceColumn = sourceColumn;
        }

        public IDictionary<string, List<ConfigurationCategoryFilterDto>> CategoryDictionary { get; }

        public string DefaultCategoryName { get; set; }

        public ConfigurationColumnDto ValueDateColumn { get; set; }
        public ConfigurationColumnDto AmountColumn { get; set; }
        public ConfigurationColumnDto TitleColumn { get; set; }
        public ConfigurationColumnDto SourceColumn { get; set; }

    }
}

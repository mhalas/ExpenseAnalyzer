using System.Collections.Generic;

namespace Shared.Configuration
{
    public class ConfigurationDto
    {
        public ConfigurationDto(
            string defaultIncomeCategoryName,
            string defaultExpenseCategoryName,

            string sourceFilesPath,
            string outputPath,
            int? splitIntoChunks,

            bool useAbsoluteValuesForAmount,
            bool generateSummary,

            IDictionary<string, List<string>> categoryDictionary)
        {
            DefaultIncomeCategoryName = defaultIncomeCategoryName;
            DefaultExpenseCategoryName = defaultExpenseCategoryName;

            SourceFilesPath = sourceFilesPath;
            OutputPath = outputPath;
            SplitIntoChunks = splitIntoChunks;
            UseAbsoluteValuesForAmount = useAbsoluteValuesForAmount;
            GenerateSummary = generateSummary;

            CategoryDictionary = categoryDictionary;
        }
        public string DefaultIncomeCategoryName { get; }
        public string DefaultExpenseCategoryName { get; }

        public string SourceFilesPath { get; }
        public string OutputPath { get; }
        public int? SplitIntoChunks { get; }

        public bool UseAbsoluteValuesForAmount { get; }
        public bool GenerateSummary { get; }

        public IDictionary<string, List<string>> CategoryDictionary { get; }
    }
}

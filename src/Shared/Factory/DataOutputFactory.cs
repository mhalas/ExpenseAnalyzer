using Shared.Configuration;
using Shared.Enum;
using Shared.Output;

namespace Shared.Factory
{
    public class DataOutputFactory
    {
        private readonly ConfigurationDto _configuration;
        private readonly string _originFilePath;

        public DataOutputFactory(ConfigurationDto configuration, string originFilePath)
        {
            _configuration = configuration;
            _originFilePath = originFilePath;
        }

        public IDataOutput GetDataOutput(OutputType type)
        {
            switch (type)
            {
                case OutputType.Excel:
                    return new ExcelFileCreator(_originFilePath);
                case OutputType.CSV:
                default:
                    return new CsvFileCreator(_configuration);


            }
        }
    }
}

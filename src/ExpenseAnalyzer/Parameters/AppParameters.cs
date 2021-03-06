using Shared.Enum;

namespace ExpenseAnalyzer.Parameters
{
    public class AppParameters
    {
        public AppParameters(string filePath, BankType bank, OutputType output)
        {
            FilePath = filePath;
            Bank = bank;
            Output = output;
        }

        public string FilePath { get; }
        public BankType Bank { get; }
        public OutputType Output { get; }
    }
}

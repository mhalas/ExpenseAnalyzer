using ExpenseAnalyzer.Exceptions;
using ExpenseAnalyzer.Parameters;
using Newtonsoft.Json;
using NLog;
using Shared.Configuration;
using Shared.Factory;
using System;
using System.Diagnostics;
using System.IO;

namespace ExpenseAnalyzer
{
    class Program
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Starting expense analyzer.");

            if(args.Length == 0)
            {
                Logger.Info("Error. No path to csv.");
                return;
            }

            if (!File.Exists("./Configuration.json"))
            {
                Logger.Info("Error. No configuration file.");
                return;
            }

            AppParameters parameters = null;
            try
            {
                parameters = new AppParametersDeserializer().Deserialize(args);
            }
            catch (ParameterException parameterException)
            {
                Logger.Error("Parameter error occured.", parameterException);
                return;
            }

            var configurationString = File.ReadAllText("./Configuration.json");
            var configuration = JsonConvert.DeserializeObject<ConfigurationDto>(configurationString);

            try
            {
                Stopwatch time = new Stopwatch();
                var bankAnalyzer = new BankFactory(configuration).GetBankAnalyzer(parameters.Bank);
                var outputLogic = new DataOutputFactory(parameters.FilePath).GetDataOutput(parameters.Output);

                if (bankAnalyzer.CanExecute())
                {
                    using (var reader = new StreamReader(parameters.FilePath))
                    {
                        Logger.Info("Start analyzing.");
                        time.Start();
                        var result = bankAnalyzer.AnalyzeExpenseHistory(reader.ReadToEnd());
                        Logger.Info("Analyze complete.");

                        outputLogic.OutputData(result).Wait();

                        time.Stop();
                        Logger.Info($"Complete in time {time.Elapsed.Hours}:{time.Elapsed.Minutes}:{time.Elapsed.Seconds}.");
                    }
                }
                else
                    Logger.Info("Analyzing not started.");

            }
            catch(Exception ex)
            {
                var oldForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Logger.Error($"Error occured! {ex.Message},\n\n{ex.StackTrace}.");

                Console.ForegroundColor = oldForegroundColor;
            }

            Logger.Info("Press any key to close...");
            Console.ReadKey();
        }
    }
}

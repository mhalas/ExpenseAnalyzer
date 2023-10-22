﻿using ExpenseAnalyzer.Exceptions;
using ExpenseAnalyzer.Parameters;
using Newtonsoft.Json;
using NLog;
using Shared.Configuration;
using Shared.Factory;
using Shared.SourceData;
using System;
using System.IO;

namespace ExpenseAnalyzer
{
    class Program
    {
        private static ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Starting expense analyzer.");

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

            if (args.Length == 0)
            {
                Logger.Info($@"Cannot find csv path parameter. Getting files from folder '{configuration.SourceFilesPath}'.");
                return;
            }

            try
            {
                var bankAnalyzer = new BankFactory(configuration).GetBankAnalyzer(parameters.Bank);
                var outputLogic = new DataOutputFactory(configuration, parameters.FilePath).GetDataOutput(parameters.Output);

                if (bankAnalyzer.CanExecute())
                {
                    var sourceDataExecutor = GetSourceDataExecutor(configuration, parameters);

                    sourceDataExecutor.Execute(bankAnalyzer, outputLogic);
                }
                else
                {
                    Logger.Info("Analyzing not started.");
                }
            }
            catch (Exception ex)
            {
                var oldForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Logger.Error($"Error occured! {ex.Message},\n\n{ex.StackTrace}.");

                Console.ForegroundColor = oldForegroundColor;
            }

            Logger.Info("Press any key to close...");
            Console.ReadKey();
        }

        private static ISourceDataExecutor GetSourceDataExecutor(ConfigurationDto configuration, AppParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.FilePath))
            {
                return new FolderDataSource(configuration.SourceFilesPath);
            }

            return new SingleFileDataSource(parameters.FilePath);
        }
    }
}

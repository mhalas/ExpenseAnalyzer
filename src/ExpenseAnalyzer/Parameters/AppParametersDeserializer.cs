﻿using ExpenseAnalyzer.Exceptions;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseAnalyzer.Parameters
{
    public class AppParametersDeserializer
    {
        private const string OutputFormatParameterName = "--output-format";
        private const string BankTypeParameterName = "--bank-type";
        private const string FileParameterName = "--file";

        public AppParameters Deserialize(IEnumerable<string> parameters)
        {
            var parametersDictionary = parameters.ToDictionary(x => x.Split("=")[0], x => x.Split("=")[1]);

            if (!parametersDictionary.ContainsKey(BankTypeParameterName)
                || !Enum.IsDefined(typeof(BankType), (object)parametersDictionary[BankTypeParameterName]))
            {
                throw new ParameterException(BankTypeParameterName);
            }

            var bankType = (BankType)Enum.Parse(typeof(BankType), parametersDictionary[BankTypeParameterName]);

            OutputType outputType = OutputType.CSV;
            if (parametersDictionary.ContainsKey(OutputFormatParameterName))
            {
                if (Enum.IsDefined(typeof(OutputType), (object)parametersDictionary[OutputFormatParameterName]))
                    outputType = (OutputType)Enum.Parse(typeof(OutputType), parametersDictionary[OutputFormatParameterName]);
                else
                    throw new ParameterException(OutputFormatParameterName);
            }


            if (!parametersDictionary.ContainsKey(FileParameterName))
            {
                return new AppParameters(null, bankType, outputType);
            }

            var file = parametersDictionary[FileParameterName];

            return new AppParameters(file, bankType, outputType);
        }
    }
}

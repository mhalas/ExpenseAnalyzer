using System;

namespace ExpenseAnalyzer.Exceptions
{
    public class ParameterException : Exception
    {
        public ParameterException(string parameterName)
            : base($"Need properly passed parameter '{parameterName}'.")
        {

        }
    }
}

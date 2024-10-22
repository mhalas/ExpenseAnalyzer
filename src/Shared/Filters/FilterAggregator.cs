using Shared.Dto;
using System.Collections.Generic;

namespace Shared.Filters
{
    public class FilterAggregator: ITransactionFilter
    {
        private readonly IEnumerable<ITransactionFilter> _filters;

        public FilterAggregator(IEnumerable<ITransactionFilter> filters)
        {
            _filters = filters;
        }

        public IEnumerable<ExpenseTransaction> FilterTransactions(IEnumerable<ExpenseTransaction> dataRow)
        {
            foreach (var filter in _filters) 
            {
                dataRow = filter.FilterTransactions(dataRow);
            }

            return dataRow;
        }
    }
}

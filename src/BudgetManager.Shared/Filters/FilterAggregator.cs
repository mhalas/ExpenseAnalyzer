using BudgetManager.Shared.Models;
using System.Collections.Generic;

namespace BudgetManager.Shared.Filters
{
    public class FilterAggregator: ITransactionFilter
    {
        private readonly IEnumerable<ITransactionFilter> _filters;

        public FilterAggregator(IEnumerable<ITransactionFilter> filters)
        {
            _filters = filters;
        }

        public IEnumerable<TransactionResultRow> FilterTransactions(IEnumerable<TransactionResultRow> dataRow)
        {
            foreach (var filter in _filters) 
            {
                dataRow = filter.FilterTransactions(dataRow);
            }

            return dataRow;
        }
    }
}

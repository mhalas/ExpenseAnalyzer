﻿using Shared.Dto;
using System.Collections.Generic;

namespace Shared.Output
{
    public interface IDataOutput
    {
        void OutputData(IEnumerable<ExpenseDataRow> data);
    }
}

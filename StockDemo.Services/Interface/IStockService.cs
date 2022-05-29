using StockDemo.Entities.DO;
using StockDemo.Entities.DTO;
using StockDemo.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Services.Interface
{
    public interface IStockService
    {
        Task<List<CalculationModel>> GetRelativeIncomeCalculation(RelativeIncomeRequest request);
    }
}

using StockDemo.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.Strategy
{
    public class ShanghaiStockExchangeIndexStrategy : IStrategy
    {
        public virtual decimal Operation(decimal num1, decimal num2)
        {
            return num1 - num2 + 1;
        }
    }
}

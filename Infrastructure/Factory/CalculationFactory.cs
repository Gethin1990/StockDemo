using StockDemo.Entities.DO;
using StockDemo.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.Factory
{
    public interface IFactory
    {
        IBaseCalculation GetInstance<T>( T paramter1, T paramter2) where T : BaseStock;
    }
    public class CalculationFactory : IFactory
    {
        public virtual IBaseCalculation GetInstance<T>(T paramter1, T paramter2) where T : BaseStock
        {
            
            return new BaseCalculation<BaseStock>(paramter1, paramter2);

        }
    }
}

using System;

namespace StockDemo.Entities.DO
{
    public class BaseCalculation<T> : IBaseCalculation where T : BaseStock
    {

        private T preStock;
        private T curStock;

        public BaseCalculation(T preStock, T curStock)
        {
            this.preStock = preStock;
            this.curStock = curStock;
        }

        public virtual decimal FluctuationRange()
        {
            if (preStock.Value == 0) { return 0; }
            return Math.Round((curStock.Value - preStock.Value) / preStock.Value, 4);
        }
    }

}

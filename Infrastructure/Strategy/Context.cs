using StockDemo.Entities.Interface;

namespace StockDemo.Entities.Strategy
{

    public class StrategyContext : IStrategyContext
    {
        private IStrategy _strategy;
        public StrategyContext(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public virtual decimal Execute(decimal num1, decimal num2)
        {
            return _strategy.Operation(num1, num2);
        }

    }
}

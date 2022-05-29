using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.DO
{

    public class CalculationModel
    {
        public BaseStock PreStock { get; set; }
        public BaseStock CurStock { get; set; }

        public BaseStock PreBaseStock { get; set; }
        public BaseStock CurBaseStock { get; set; }
        
        public decimal FluctuationRange { get; set; }
        public decimal BaseFluctuationRange { get; set; }

        public decimal PreRelativeIncome { get; set; }
        public decimal RelativeIncome { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.DTO
{
    public class RelativeIncomeResponse
    {
        public List<string> Date { get; set; }

        public List<decimal> RelativeIncome { get; set; }

        public string Code { get; set; }
    }
}

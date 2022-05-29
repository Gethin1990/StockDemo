using StockDemo.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.DTO
{
    public class RelativeIncomeRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<StockCodeEnum> StockType { get; set; }
    }
}

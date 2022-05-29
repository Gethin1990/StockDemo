using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDemoMvc.Models
{
    public class StockViewModel
    {
        public string[] SelectStocks { get; set; }
        public IEnumerable<SelectListItem> AllStocks { get; set; }
    }
}

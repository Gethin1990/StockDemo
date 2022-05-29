using Microsoft.AspNetCore.Mvc;
using StockDemo.Entities.DTO;
using StockDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IStockService _service;
        public StockController(IStockService stockService)
        {
            _service = stockService;
        }
        [HttpPost("relativeIncomeCalculation")]
        public ActionResult<List<RelativeIncomeResponse>> RelativeIncomeCalculation(RelativeIncomeRequest request)
        {
            var response = new List<RelativeIncomeResponse>();

            


            var result = _service.GetRelativeIncomeCalculation(request);

            var group = result.Result.GroupBy(t => t.Code);

            foreach (var item in group)
            {
                var res = new RelativeIncomeResponse();
                foreach (var i in item)
                {
                    res.Date.Append(i.Date.ToString("yyyy-MM-dd"));
                    res.RelativeIncome.Append(i.RelativeIncome);
                    res.Code = i.Code.ToString("000000");
                }
                response.Add(res);
            }

            return response;

        }


    }
}

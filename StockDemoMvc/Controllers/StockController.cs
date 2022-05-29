using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StockDemo.Entities.DTO;
using StockDemo.Entities.Enum;
using StockDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDemoMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : Controller
    {
        private IStockService _service;
        private readonly ILogger<HomeController> _logger;
        public StockController(IStockService service, ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpPost("relativeIncomeCalculation")]
        public ActionResult<List<RelativeIncomeResponse>> RelativeIncomeCalculation(RelativeIncomeRequest request)
        {
            var response = new List<RelativeIncomeResponse>()
            {
                new RelativeIncomeResponse(){ Code = ((int)StockCodeEnum.PingAnBank).ToString(), Date = new List<string>(), RelativeIncome = new List<decimal>() },
                new RelativeIncomeResponse(){ Code = ((int)StockCodeEnum.MaoTaiGuiZhou).ToString(), Date = new List<string>(), RelativeIncome = new List<decimal>() },
                new RelativeIncomeResponse(){ Code = ((int)StockCodeEnum.ZhongXinBuilding).ToString(), Date = new List<string>(), RelativeIncome = new List<decimal>() },
                new RelativeIncomeResponse(){ Code = ((int)StockCodeEnum.HuaXingYuanChuang).ToString(), Date = new List<string>(), RelativeIncome = new List<decimal>() },
                new RelativeIncomeResponse(){ Code = ((int)StockCodeEnum.TongDaChuangYe).ToString(), Date = new List<string>(), RelativeIncome = new List<decimal>() }
            };




            var result = _service.GetRelativeIncomeCalculation(request);

            var group = result.Result.GroupBy(t => t.Code);

            foreach (var item in group)
            {
               
                foreach (var i in item)
                {
                    var res = response.FirstOrDefault(t => t.Code == i.Code.ToString());
                    res.Date.Add(i.Date.ToString("yyyy-MM-dd"));
                    res.RelativeIncome.Add(i.RelativeIncome);
                }
            }

            return response;

        }


    }
}

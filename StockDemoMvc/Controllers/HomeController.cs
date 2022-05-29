using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StockDemoMvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StockDemoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {

            StockViewModel vm = new StockViewModel();
            vm.SelectStocks = new string[] { };
            vm.AllStocks = GetAllStocks();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<SelectListItem> GetAllStocks()
        {
            List<SelectListItem> allStocks = new List<SelectListItem>();
            allStocks.Add(new SelectListItem() { Value = "000001", Text = "平安银行" });
            allStocks.Add(new SelectListItem() { Value = "600519", Text = "贵州茅台" });
            allStocks.Add(new SelectListItem() { Value = "601066", Text = "中信建设" });
            allStocks.Add(new SelectListItem() { Value = "688001", Text = "华兴源创" });
            allStocks.Add(new SelectListItem() { Value = "600647", Text = "同达创业" });

            return allStocks.AsEnumerable();
        }
    }
}

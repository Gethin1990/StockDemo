using StockDemo.Entities.DO;
using StockDemo.Entities.Factory;
using StockDemo.Entities.Strategy;
using StockDemo.Entities.Enum;
using StockDemo.Entities.Interface;
using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using StockDemo.Entities.DTO;
using StockDemo.Infrastructure.Storage;
using System.Linq;
using Microsoft.Extensions.Configuration;
using StockDemo.Services.Interface;
using System.Threading.Tasks;

namespace StockDemo.Services
{
    public class StockService : IStockService
    {
        private readonly IFactory _factory;
        private readonly IStrategyContext _strategyContext;
        private readonly IMemoryCache _cache;
        private readonly Storage _storage;
        private readonly IConfiguration _configuration;
        private readonly StockCodeEnum baseType = StockCodeEnum.ShanghaiCompositeIndex;

        public StockService(IFactory factory, IStrategyContext strategyContext, IMemoryCache cache, IConfiguration configuration)
        {
            _factory = factory;
            _strategyContext = strategyContext;
            _cache = cache;
            _configuration = configuration;
            var storagePath = _configuration.GetSection("StoragePath").Value;
            _storage = Storage.GetInstance(storagePath);
        }


        public async Task<List<CalculationModel>> GetRelativeIncomeCalculation(RelativeIncomeRequest request)
        {
            var result = new List<CalculationModel>();
            foreach (var type in request.StockType)
            {

                var cacheKey = request.StartTime.ToString("yyyy-MM-dd") + type.ToString();
                // no cache
                var res = _cache.Get<List<CalculationModel>>(cacheKey);
                if (res == null || res.Count() == 0)
                {
                    var data = CalculateModel(request, type);
                    _cache.Set(cacheKey, data);
                }
                else
                {
                    // cache<EndTime: update cache
                    if (res.LastOrDefault().Date < request.EndTime)
                    {
                        var data = CalculateModel(request, type);
                        _cache.Set(cacheKey, data);
                    }
                }
                res = _cache.Get<List<CalculationModel>>(cacheKey);
                var r = res?.Where(t => t.Date <= request.EndTime)?.ToList();
                if (r != null) { result.AddRange(r); }
                
            }
            return result;
        }


        public List<CalculationModel> CalculateModel(RelativeIncomeRequest request, StockCodeEnum curType)
        {
            var result = new List<CalculationModel>();
            Func<BaseStock, bool> func = x => x.Date >= request.StartTime && x.Date <= request.EndTime && x.Code == (int)curType;
            var data = _storage.Get(func).OrderBy(t => t.Date).ToList();
            Func<BaseStock, bool> func2 = x => x.Date >= request.StartTime && x.Date <= request.EndTime && x.Code == (int)baseType;
            var baseData = _storage.Get(func2).OrderBy(t => t.Date).ToList();

            if (data.Count == 0 || baseData.Count == 0)
            {
                return result;
            }


            BaseStock preBaseStock = baseData[0];
            BaseStock preStock = data[0];
            decimal preRelativeIncome = 1;

            var count = baseData.Count();
            for (var i = 0; i < count; i++)
            {
                var model = new CalculationModel()
                {
                    Code = data[i].Code,
                    Name = data[i].Name,
                    Date = baseData[i].Date,
                    PreBaseStock = preBaseStock,
                    PreStock = preStock,
                    CurBaseStock = baseData[i],
                    CurStock = data[i],
                    PreRelativeIncome = preRelativeIncome
                };
                var res = Calculate(model);
                result.Add(res);

                preBaseStock = res.CurBaseStock;
                preStock = res.CurStock;
                preRelativeIncome = res.RelativeIncome;

            }
            return result;

        }

        public CalculationModel Calculate(CalculationModel calculationModel)
        {
            IBaseCalculation curCal = _factory.GetInstance(calculationModel.PreStock, calculationModel.CurStock);
            IBaseCalculation baseCal = _factory.GetInstance(calculationModel.PreBaseStock, calculationModel.CurBaseStock);

            calculationModel.FluctuationRange = curCal.FluctuationRange();
            calculationModel.BaseFluctuationRange = baseCal.FluctuationRange();


            calculationModel.PreRelativeIncome = calculationModel.PreRelativeIncome == 0 ? 1 : calculationModel.PreRelativeIncome;
            var range = _strategyContext.Execute(calculationModel.FluctuationRange, calculationModel.BaseFluctuationRange);
            calculationModel.RelativeIncome = Math.Round(range * calculationModel.PreRelativeIncome, 2);

            return calculationModel;
        }
    }
}

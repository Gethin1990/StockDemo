using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using StockDemo.Entities.DO;
using StockDemo.Entities.DTO;
using StockDemo.Entities.Enum;
using StockDemo.Entities.Factory;
using StockDemo.Entities.Interface;
using StockDemo.Entities.Strategy;
using StockDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemoTest
{
    public class ServicesTest
    {
        private IFactory factory;
        private IStrategyContext strategyContext;
        private Mock<IMemoryCache> cache;
        private Mock<IConfiguration> config;
        [SetUp]
        public void Setup()
        {
            factory = new CalculationFactory();
            var strategy = new ShanghaiStockExchangeIndexStrategy();
            strategyContext = new StrategyContext(strategy);
            cache = new Mock<IMemoryCache>();
            config = new Mock<IConfiguration>();
            config.Setup(_ => _.GetSection("StoragePath").Value).Returns(@"..\net5.0\Data\data.csv");


        }
        [Test]
        public void TestCalculate_SameDay()
        {
            var service = new StockService(factory, strategyContext, cache.Object, config.Object);
            var calculationModel = buildModel();
            calculationModel.CurBaseStock = new ShanghaiCompositeIndex() { Code = ((int)StockCodeEnum.ShanghaiCompositeIndex), Name = StockCodeEnum.ShanghaiCompositeIndex.ToString(), Value = 2994.01M, Date = Convert.ToDateTime("2019-03-01") };
            calculationModel.CurStock = new MaoTaiGuiZhouStock() { Code = ((int)StockCodeEnum.MaoTaiGuiZhou), Name = StockCodeEnum.MaoTaiGuiZhou.ToString(), Value = 789.30M, Date = Convert.ToDateTime("2019-03-01") };



            var result = service.Calculate(calculationModel);
            Assert.AreEqual(result.RelativeIncome, 1);
        }

        [Test]
        public void TestCalculate_Success()
        {
            var service = new StockService(factory, strategyContext, cache.Object,config.Object);
            var calculationModel = buildModel();


            var result = service.Calculate(calculationModel);
            Assert.AreEqual(result.RelativeIncome, 0.98);
        }

        [Test]
        public void TestCalculateModel_Success()
        {
            var service = new StockService(factory, strategyContext, cache.Object, config.Object);
            var request = new RelativeIncomeRequest()
            {
                StartTime = DateTime.Parse("2019-03-01"),
                EndTime = DateTime.Parse("2019-03-20"),
                StockType = new List<StockCodeEnum>() { StockCodeEnum.MaoTaiGuiZhou}

            };
            var result = service.CalculateModel(request, StockCodeEnum.MaoTaiGuiZhou);
        }
        private CalculationModel buildModel()
        {
            var result = new CalculationModel();

            var curType = StockCodeEnum.MaoTaiGuiZhou;
            var baseType = StockCodeEnum.ShanghaiCompositeIndex;



            result.PreBaseStock = new ShanghaiCompositeIndex() { Code = ((int)baseType), Name = baseType.ToString(), Value = 2994.01M, Date = Convert.ToDateTime("2019-03-01") };
            result.PreStock = new MaoTaiGuiZhouStock() { Code = ((int)curType), Name = curType.ToString(), Value = 789.30M, Date = Convert.ToDateTime("2019-03-01") };
            result.CurBaseStock = new ShanghaiCompositeIndex() { Code = ((int)baseType), Name = baseType.ToString(), Value = 3027.58M, Date = Convert.ToDateTime("2019-03-04") };
            result.CurStock = new MaoTaiGuiZhouStock() { Code = ((int)curType), Name = curType.ToString(), Value = 781.86M, Date = Convert.ToDateTime("2019-03-04") };
            return result;
        }

    }
}

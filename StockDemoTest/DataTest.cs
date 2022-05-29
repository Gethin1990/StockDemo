
using NUnit.Framework;
using StockDemo.Entities.Repository;

namespace StockDemoTest
{
    public class DataTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var dataPath = @"..\net5.0\Data\data.csv";
            var data = CsvHelper.OpenCSV(dataPath);

            var list = RepoHelper.CoverTableToBaseStock(data);
            Assert.IsTrue(list.Count != 0);
        }
    }
}
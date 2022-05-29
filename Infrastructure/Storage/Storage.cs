using StockDemo.Entities.DO;
using StockDemo.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Infrastructure.Storage
{
    public sealed class Storage
    {
        private List<BaseStock> list = new List<BaseStock>();
        private static Storage _instance = null;
        private static readonly object _Object = new object();
        static Storage() { }
        private Storage(string dataPath) {

            var data = CsvHelper.OpenCSV(dataPath);

            list = RepoHelper.CoverTableToBaseStock(data);
        }
        public static Storage GetInstance(string dataPath)
        {
            if (_instance == null)
            {
                lock (_Object)
                {
                    if (_instance == null)
                    {
                        _instance = new Storage(dataPath);
                    }
                }
            }
            return _instance;
        }

        public IEnumerable<BaseStock> GetAll()
        {
            return list;
        }

        public IEnumerable<BaseStock> Get(Func<BaseStock,bool> func)
        {
            return list.Where(func);
        }

    }


}

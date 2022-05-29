using StockDemo.Entities.DO;
using StockDemo.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.Repository
{
    public static class RepoHelper
    {

        public static List<BaseStock> CoverTableToBaseStock(DataTable dt)
        {
            var result = new List<BaseStock>();

            string date=null;
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Date")
                    {
                        date = (string)row[col];
                    }
                    else
                    {
                        var model = new BaseStock();
                        model.Code = int.Parse(col.ColumnName);
                        model.Name = ((StockCodeEnum)model.Code).ToString();
                        var val = string.IsNullOrEmpty((string)row[col]) ? "0" : (string)row[col];
                        model.Value = decimal.Parse(val);
                        model.Date = DateTime.Parse(date);
                        result.Add(model);
                    }
                }

            }
            return result;



        }

        public static List<T> TableToList<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] properties = type.GetProperties();
                T model = new T();
                foreach (PropertyInfo p in properties)
                {
                    object value = row[p.Name];
                    if (value == DBNull.Value)
                    {
                        p.SetValue(model, "", null);
                    }
                    else
                    {
                        if (value is decimal)
                        {
                            p.SetValue(model, Convert.ToInt32(value), null);
                        }
                        else
                        {
                            p.SetValue(model, value, null);
                        }
                    }
                }
                list.Add(model);
            }
            return list;
        }
    }
}

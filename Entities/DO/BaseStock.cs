using System;

namespace StockDemo.Entities.DO
{
	public class BaseStock
	{
		public int Code { get; set; }
		public string Name { get; set; }
		public decimal Value { get; set; }
		public DateTime Date { get; set; }
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDemo.Entities.Interface
{
    public interface IStrategy
    {
        decimal Operation(decimal num1, decimal num2);
    }
}

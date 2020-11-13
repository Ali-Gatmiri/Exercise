using System;
using System.Collections.Generic;
using System.Text;

namespace Tecsys.Exercise.Application.Products.Queries
{
    public class ProductVm
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public string CategoryName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int Discount { get; set; }
        public Tax Tax { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}

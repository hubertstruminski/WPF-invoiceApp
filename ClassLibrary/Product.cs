﻿using System.Collections.Generic;

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
        public long TaxId { get; set; }
        public Tax Tax { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}

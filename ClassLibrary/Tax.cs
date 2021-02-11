using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Tax
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaxAmount { get; set; }
        public List<Product> Products { get; set; }
    }
}

using System.Collections.Generic;

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

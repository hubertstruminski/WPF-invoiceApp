using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Invoice
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public string Comment { get; set; }
    }
}

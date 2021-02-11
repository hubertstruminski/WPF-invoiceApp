using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Address
    {
        public long Id { get; set; }
        public string AddressName { get; set; }
        public string Country { get; set; }
        public Customer Customer { get; set; }
    }
}

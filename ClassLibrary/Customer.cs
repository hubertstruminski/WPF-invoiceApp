﻿using System.Collections.Generic;

namespace ClassLibrary
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Nip { get; set; }
        public string Note { get; set; }
        public long AddressId { get; set; }
        public Address Address { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}

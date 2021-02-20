using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class TaxRepository
    {
        public void RemoveTax(Tax selectedItem, DatabaseContext _context)
        {
            _context.Taxes.Remove(selectedItem);
            _context.SaveChanges();
        }
    }
}

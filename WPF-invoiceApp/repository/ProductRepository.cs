using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class ProductRepository
    {
        public void RemoveProduct(Product selectedItem, DatabaseContext _context)
        {
            _context.Products.Remove(selectedItem);
            _context.SaveChanges();
        }

        public Product FindByIdWithTaxAndInvoiceProducts(Product selectedItem, DatabaseContext _context)
        {
            return _context.Products.Include("Tax").Include("InvoiceProducts").Where(x => x.Id == selectedItem.Id).Single();
        }
    }
}

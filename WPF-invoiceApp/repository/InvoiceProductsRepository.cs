using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class InvoiceProductsRepository
    {
        public List<InvoiceProduct> FindInvoiceProductsWithProductAndInvoiceById(Invoice selectedItem, DatabaseContext _context)
        {
            return _context.InvoiceProducts.Include("Product").Include("Invoice").Where(x => x.InvoiceId == selectedItem.Id).ToList();
        }

        public List<InvoiceProduct> FindInvoiceProductsWithProductAndInvoiceByProductId(Product selectedItem, DatabaseContext _context)
        {
            return _context.InvoiceProducts.Include("Product").Include("Invoice").Where(x => x.ProductId == selectedItem.Id).ToList();
        }
    }
}

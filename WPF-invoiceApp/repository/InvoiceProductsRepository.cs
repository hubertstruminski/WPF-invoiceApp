using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class InvoiceProductsRepository
    {
        /// <summary>
        /// Finds List of InvoiceProduct entities by id
        /// </summary>
        /// <remarks>
        /// with included Product and Invoice object
        /// </remarks>
        /// <param name="selectedItem">Selected Invoice entity from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        /// <returns>Returns list of InvoiceProduct entities</returns>
        public List<InvoiceProduct> FindInvoiceProductsWithProductAndInvoiceById(Invoice selectedItem, DatabaseContext _context)
        {
            return _context.InvoiceProducts.Include("Product").Include("Invoice").Where(x => x.InvoiceId == selectedItem.Id).ToList();
        }

        /// <summary>
        /// Finds List of InvoiceProduct entities by product id
        /// </summary>
        /// <param name="selectedItem">Product object</param>
        /// <param name="_context">DbContext object</param>
        /// <returns>Returns list of InvoiceProduct entities</returns>
        public List<InvoiceProduct> FindInvoiceProductsWithProductAndInvoiceByProductId(Product selectedItem, DatabaseContext _context)
        {
            return _context.InvoiceProducts.Include("Product").Include("Invoice").Where(x => x.ProductId == selectedItem.Id).ToList();
        }
    }
}

using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class ProductRepository
    {
        /// <summary>
        /// Removes product entity from Products DbSet
        /// </summary>
        /// <param name="selectedItem">Selected product object from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        public void RemoveProduct(Product selectedItem, DatabaseContext _context)
        {
            _context.Products.Remove(selectedItem);
            _context.SaveChanges();
        }

        /// <summary>
        /// Finds Product entity by id
        /// </summary>
        /// <remarks>
        /// with included Tax object and InvoiceProducts list of objects
        /// </remarks>
        /// <param name="selectedItem">Product object</param>
        /// <param name="_context">DbContext object</param>
        /// <returns>Returns Product entity</returns>
        public Product FindByIdWithTaxAndInvoiceProducts(Product selectedItem, DatabaseContext _context)
        {
            return _context.Products.Include("Tax").Include("InvoiceProducts").Where(x => x.Id == selectedItem.Id).Single();
        }

        /// <summary>
        /// Adds Product entity to Products DbSet
        /// </summary>
        /// <param name="product">New Product object</param>
        /// <param name="context">DbContext object</param>
        /// <param name="isUpdateFlag">isUpdateFlag specify whether add or update entity</param>
        public void AddProduct(Product product, DatabaseContext context, bool isUpdateFlag)
        {
            if (isUpdateFlag)
                context.Products.Update(product);
            else
                context.Products.Add(product);

            context.SaveChanges();
        }
    }
}

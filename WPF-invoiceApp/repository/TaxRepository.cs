using ClassLibrary;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class TaxRepository
    {
        /// <summary>
        /// Removes tax entity from Taxes DbSet
        /// </summary>
        /// <param name="selectedItem">Selected tax object from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        public void RemoveTax(Tax selectedItem, DatabaseContext _context)
        {
            _context.Taxes.Remove(selectedItem);
            _context.SaveChanges();
        }

        /// <summary>
        /// Adds Tax entity to Taxes DbSet
        /// </summary>
        /// <param name="tax">New Tax object</param>
        /// <param name="context">DbContext object</param>
        /// <param name="isUpdateFlag">isUpdateFlag specify whether add or update entity</param>
        public void AddTax(Tax tax, DatabaseContext context, bool isUpdateFlag)
        {
            if (isUpdateFlag)
                context.Taxes.Update(tax);
            else
                context.Taxes.Add(tax);

            context.SaveChanges();
        }
    }
}

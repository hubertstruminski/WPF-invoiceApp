using ClassLibrary;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class InvoiceRepository
    {
        /// <summary>
        /// Removes invoice entity from Invoices DbSet
        /// </summary>
        /// <param name="selectedItem">Selected Invoice entity from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        public void RemoveInvoice(Invoice selectedItem, DatabaseContext _context)
        {
            _context.Invoices.Remove(selectedItem);
            _context.SaveChanges();
        }

        /// <summary>
        /// Adds Invoice entity to Invoices DbSet
        /// </summary>
        /// <param name="invoice">New invoice object</param>
        /// <param name="context">DbContext object</param>
        /// <param name="isUpdateFlag">isUpdate specify whether add or update entity</param>
        public void AddInvoice(Invoice invoice, DatabaseContext context, bool isUpdateFlag)
        {
            if (isUpdateFlag)
                context.Invoices.Update(invoice);
            else
                context.Invoices.Add(invoice);

            context.SaveChanges();
        }
    }
}

using ClassLibrary;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class InvoiceRepository
    {
        public void RemoveInvoice(Invoice selectedItem, DatabaseContext _context)
        {
            _context.Invoices.Remove(selectedItem);
            _context.SaveChanges();
        }

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

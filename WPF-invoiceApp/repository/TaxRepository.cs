using ClassLibrary;
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

using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class CustomerRepository
    {
        public void RemoveCustomer(Customer selectedItem, DatabaseContext _context)
        {
            _context.Customers.Remove(selectedItem);
            _context.SaveChanges();
        }

        public Customer FindCustomerInvoicesById(Customer selectedItem, DatabaseContext _context)
        {
            return _context.Customers.Include("Invoices").Where(x => x.Id == selectedItem.Id).Single();
        }

        public Customer FindCustomerAddressById(Invoice selectedItem, DatabaseContext _context)
        {
            return _context.Customers.Include("Address").Where(x => x.Id == selectedItem.Customer.Id).Single();
        }
    }
}

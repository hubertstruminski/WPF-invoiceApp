using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class CustomerRepository
    {
        /// <summary>
        /// Removes customer entity from Customers DbSet
        /// </summary>
        /// <param name="selectedItem">Selected customer object from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        public void RemoveCustomer(Customer selectedItem, DatabaseContext _context)
        {
            _context.Customers.Remove(selectedItem);
            _context.SaveChanges();
        }

        /// <summary>
        /// Finds customer entity via id
        /// </summary>
        /// <remarks>
        /// with included Invoices List object
        /// </remarks>
        /// <param name="selectedItem">Selected customer object from DataGrid</param>
        /// <param name="_context">DbContext object</param>
        /// <returns>Returns Customer entity</returns>
        public Customer FindCustomerInvoicesById(Customer selectedItem, DatabaseContext _context)
        {
            return _context.Customers.Include("Invoices").Where(x => x.Id == selectedItem.Id).Single();
        }

        /// <summary>
        /// Finds customer entity via id
        /// </summary>
        /// <remarks>
        /// with included Address object
        /// </remarks>
        /// <param name="selectedItem">Invoice entity</param>
        /// <param name="_context">DbContext object</param>
        /// <returns>Returns Customer entity</returns>
        public Customer FindCustomerAddressById(Invoice selectedItem, DatabaseContext _context)
        {
            return _context.Customers.Include("Address").Where(x => x.Id == selectedItem.Customer.Id).Single();
        }

        /// <summary>
        /// Adds Customer entity to Customers DbSet
        /// </summary>
        /// <param name="customer">New Customer object</param>
        /// <param name="context">DbContext object</param>
        /// <param name="isUpdateFlag">isUpdateFlag specify whether add or update entity</param>
        public void AddCustomer(Customer customer, DatabaseContext context, bool isUpdateFlag)
        {
            if (isUpdateFlag)
                context.Customers.Update(customer);
            else
                context.Customers.Add(customer);

            context.SaveChanges();
        }
    }
}

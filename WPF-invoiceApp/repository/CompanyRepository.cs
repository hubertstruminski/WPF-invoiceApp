using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.repository
{
    public class CompanyRepository
    {
        public void RemoveCompany(Company selectedItem, DatabaseContext _context)
        {
            _context.Companies.Remove(selectedItem);
            _context.SaveChanges();
        }
    }
}

using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}

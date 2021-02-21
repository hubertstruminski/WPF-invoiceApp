using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_invoiceApp.context;
using WPF_invoiceApp.repository;
using WPF_invoiceApp.service;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.template.dashboards
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource invoiceViewSource;

        private readonly InvoiceRepository repository = new InvoiceRepository();
        private readonly InvoiceProductsRepository invoiceProductsRepository = new InvoiceProductsRepository();
        private readonly CustomerRepository customerRepository = new CustomerRepository();
        private readonly InvoiceService service = new InvoiceService();

        public InvoiceWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            invoiceViewSource = (CollectionViewSource) FindResource(nameof(invoiceViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Customers.Load();
            _context.Taxes.Load();
            _context.Products.Load();
            _context.Invoices.Load();

            invoiceViewSource.Source = _context.Invoices.Local.ToObservableCollection();
        }

        private void InvoiceDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Invoice selectedItem = (Invoice) invoiceDataGrid.SelectedItem;

            _context.Invoices.Load();
            repository.RemoveInvoice(selectedItem, _context);

            RefreshInvoiceGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            _context.Taxes.Load();
            _context.Products.Load();
            _context.Invoices.Load();
            
            Invoice selectedItem = (Invoice) invoiceDataGrid.SelectedItem;
            selectedItem.Customer = customerRepository.FindCustomerAddressById(selectedItem, _context);

            NewInvoiceWindow newInvoiceWindow = new NewInvoiceWindow(selectedItem, this, _context);
            newInvoiceWindow.ShowDialog();
        }

        /// <summary>
        /// Refresh GridData view with Invoice objects
        /// </summary>
        public void RefreshInvoiceGridData()
        {
            _context.Invoices.Load();
            invoiceDataGrid.ItemsSource = null;
            DbSet<Invoice> invoices = _context.Invoices;
            invoiceDataGrid.Items.Clear();
            foreach (Invoice x in invoices)
            {
                invoiceDataGrid.Items.Add(x);
            }
            invoiceDataGrid.Items.Refresh();
        }

        private void Button_Show_Click(object sender, RoutedEventArgs e)
        {
            Invoice selectedItem = (Invoice)invoiceDataGrid.SelectedItem;

            selectedItem.InvoiceProducts = invoiceProductsRepository.FindInvoiceProductsWithProductAndInvoiceById(selectedItem, _context);
            selectedItem.Customer = customerRepository.FindCustomerAddressById(selectedItem, _context);

            InvoiceDetailsWindow invoiceDetailsWindow = new InvoiceDetailsWindow(selectedItem);
            service.OnSubViewDetailsShow(invoiceDetailsWindow, RightViewBox);
        }
    }
}

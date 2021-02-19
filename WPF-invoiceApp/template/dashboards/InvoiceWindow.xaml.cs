using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.template.dashboards
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private CollectionViewSource invoiceViewSource;

        public InvoiceWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            invoiceViewSource = (CollectionViewSource) FindResource(nameof(invoiceViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            _context.Customers.Load();
            _context.Taxes.Load();
            _context.Products.Load();
            _context.Invoices.Load();

            invoiceViewSource.Source = _context.Invoices.Local.ToObservableCollection();
        }

        private void OnSelectItem(object sender, SelectionChangedEventArgs e)
        {
            Invoice selectedItem = (Invoice) invoiceDataGrid.SelectedItem;
        }

        private void InvoiceDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Invoice selectedItem = (Invoice) invoiceDataGrid.SelectedItem;

            _context.Invoices.Load();

            _context.Invoices.Remove(selectedItem);
            _context.SaveChanges();

            // MUST BE FOR REFRESH COUNTER COLUMN AFTER PERFORM DELETE ACTION
            RefreshInvoiceGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            _context.Taxes.Load();
            _context.Products.Load();
            _context.Invoices.Load();
            
            Invoice selectedItem = (Invoice) invoiceDataGrid.SelectedItem;

            Customer customer = _context.Customers.Include("Address").Where(x => x.Id == selectedItem.Customer.Id).Single();
            selectedItem.Customer = customer;

            NewInvoiceWindow newInvoiceWindow = new NewInvoiceWindow(selectedItem, this, _context);
            newInvoiceWindow.ShowDialog();
        }

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

            List<InvoiceProduct> invoiceProducts = _context.InvoiceProducts.Include("Product").Include("Invoice").Where(x => x.InvoiceId == selectedItem.Id).ToList();
            Customer foundCustomer = _context.Customers.Include("Address").Where(x => x.Id == selectedItem.CustomerId).Single();

            selectedItem.InvoiceProducts = invoiceProducts;
            selectedItem.Customer = foundCustomer;

            InvoiceDetailsWindow invoiceDetailsWindow = new InvoiceDetailsWindow(selectedItem, _context);

            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(invoiceDetailsWindow);
        }
    }
}

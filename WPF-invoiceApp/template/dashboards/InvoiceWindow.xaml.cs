using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace WPF_invoiceApp.template.dashboards
{
    /// <summary>
    /// Logika interakcji dla klasy InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : UserControl
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private CollectionViewSource invoiceViewSource;

        public InvoiceWindow()
        {
            InitializeComponent();
            invoiceViewSource = (CollectionViewSource) FindResource(nameof(invoiceViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            DbSet<Invoice> invoices = _context.Invoices;
            foreach (Invoice x in invoices)
            {
                _context.Invoices.Remove(x);
            }

            _context.SaveChanges();
            _context.Invoices.Load();

            invoiceViewSource.Source = _context.Invoices.Local.ToObservableCollection();

            DbSet<Customer> customers = _context.Customers;
            Customer customer = null;
            foreach(Customer x in customers)
            {
                customer = x;
                break;
            }

            DbSet<Product> products = _context.Products;
            Product product = null;
            foreach(Product x in products)
            {
                product = x;
                break;
            }

            _context.Invoices.Add(new Invoice()
            {
                Number = "876356",
                Date = DateTime.Now,
                Deadline = DateTime.Now,
                Status = Status.NOT_SENT,
                Customer = customer,
                Products = new List<Product>() { product },
                Comment = "This is a comment."
            });
            _context.Invoices.Add(new Invoice() {
                Number = "123356",
                Date = DateTime.Now,
                Deadline = DateTime.Now,
                Status = Status.SENT,
                Customer = customer,
                Products = new List<Product>() { product },
                Comment = "This is a comment 222."
            });

            _context.SaveChanges();
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
            Invoice selectedItem = (Invoice) invoiceDataGrid.SelectedItem;

            NewInvoiceWindow newInvoiceWindow = new NewInvoiceWindow(selectedItem, this);
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
    }
}

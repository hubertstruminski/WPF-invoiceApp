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
    /// Logika interakcji dla klasy CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private CollectionViewSource customerViewSource;

        public CustomerWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            customerViewSource = (CollectionViewSource)FindResource(nameof(customerViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            _context.Addresses.Load();
            _context.Customers.Load();

            customerViewSource.Source = _context.Customers.Local.ToObservableCollection();
        }

        private void OnSelectItem(object sender, SelectionChangedEventArgs e)
        {
            Customer selectedItem = (Customer) customerDataGrid.SelectedItem;
        }

        private void CustomerDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedItem = (Customer) customerDataGrid.SelectedItem;

            _context.Customers.Load();

            _context.Customers.Remove(selectedItem);
            _context.SaveChanges();

            // MUST BE FOR REFRESH COUNTER COLUMN AFTER PERFORM DELETE ACTION
            RefreshCustomerGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            _context.Addresses.Load();
            Customer selectedItem = (Customer) customerDataGrid.SelectedItem;

            NewCustomerWindow newCustomerWindow = new NewCustomerWindow(selectedItem, _context, this);
            newCustomerWindow.ShowDialog();
        }

        public void RefreshCustomerGridData()
        {
            _context.Customers.Load();
            customerDataGrid.ItemsSource = null;
            DbSet<Customer> customers = _context.Customers;
            customerDataGrid.Items.Clear();
            foreach (Customer x in customers)
            {
                customerDataGrid.Items.Add(x);
            }
            customerDataGrid.Items.Refresh();
        }

        private void Button_Show_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedItem = (Customer)customerDataGrid.SelectedItem;

            Customer foundCustomer = _context.Customers.Include("Invoices").Where(x => x.Id == selectedItem.Id).Single();

            //List<InvoiceProduct> invoiceProducts = _context.InvoiceProducts.Include("Product").Include("Invoice").Where(x => x.ProductId == selectedItem.Id).ToList();

            //Product foundProduct = _context.Products.Include("Tax").Include("InvoiceProducts").Where(x => x.Id == selectedItem.Id).Single();
            //foundProduct.InvoiceProducts = invoiceProducts;

            CustomerDetailsWindow customerDetailsWindow = new CustomerDetailsWindow(foundCustomer, _context);

            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(customerDetailsWindow);
        }
    }
}

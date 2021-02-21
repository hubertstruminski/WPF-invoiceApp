using ClassLibrary;
using Microsoft.EntityFrameworkCore;
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
    /// Logika interakcji dla klasy CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource customerViewSource;

        private readonly CustomerRepository repository = new CustomerRepository();
        private readonly CustomerService service = new CustomerService();

        public CustomerWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            customerViewSource = (CollectionViewSource) FindResource(nameof(customerViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Addresses.Load();
            _context.Customers.Load();

            customerViewSource.Source = _context.Customers.Local.ToObservableCollection();
        }

        private void CustomerDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedItem = (Customer) customerDataGrid.SelectedItem;

            _context.Customers.Load();
            repository.RemoveCustomer(selectedItem, _context);

            RefreshCustomerGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            _context.Addresses.Load();
            Customer selectedItem = (Customer) customerDataGrid.SelectedItem;

            NewCustomerWindow newCustomerWindow = new NewCustomerWindow(selectedItem, _context, this);
            newCustomerWindow.ShowDialog();
        }

        /// <summary>
        /// Refresh GridData view with Customer objects
        /// </summary>
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
            Customer foundCustomer = repository.FindCustomerInvoicesById(selectedItem, _context);
            CustomerDetailsWindow customerDetailsWindow = new CustomerDetailsWindow(foundCustomer);

            service.OnSubViewDetailsShow(customerDetailsWindow, RightViewBox);
        }
    }
}

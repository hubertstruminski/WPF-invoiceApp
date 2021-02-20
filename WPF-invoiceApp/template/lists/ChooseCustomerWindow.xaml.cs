using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template.lists
{
    /// <summary>
    /// Logika interakcji dla klasy ChooseCustomerWindow.xaml
    /// </summary>
    public partial class ChooseCustomerWindow : Window
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource customerViewSource;
        private readonly NewInvoiceWindow newInvoiceWindow;
        
        public ChooseCustomerWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            customerViewSource = (CollectionViewSource) FindResource(nameof(customerViewSource));
        }

        public ChooseCustomerWindow(NewInvoiceWindow newInvoiceWindow, DatabaseContext context) : this(context)
        {
            this.newInvoiceWindow = newInvoiceWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            _context.Addresses.Load();
            _context.Customers.Load();

            customerViewSource.Source = _context.Customers.Local.ToObservableCollection();
        }

        private void CustomerDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        public void RefreshCustomerGridData()
        {
            _context.Addresses.Load();
            _context.Customers.Load();

            customerDataGrid.ItemsSource = null;
            customerDataGrid.Items.Clear();

            DbSet<Customer> customers = _context.Customers;
            foreach (Customer x in customers)
            {
                customerDataGrid.Items.Add(x);
            }
            customerDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedItem = (Customer)customerDataGrid.SelectedItem;
            newInvoiceWindow.SetCustomer(selectedItem);
            this.Close();
        }
    }
}

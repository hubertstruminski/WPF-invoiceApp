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

            //DbSet<Customer> customers = _context.Customers;
            //foreach (Customer x in customers)
            //{
            //    _context.Customers.Remove(x);
            //}

            //_context.SaveChanges();

            _context.Addresses.Load();
            _context.Customers.Load();

            customerViewSource.Source = _context.Customers.Local.ToObservableCollection();

            //Address address1 = new Address() { AddressName = "Spokojna 24/7B", Country = "Poland" };
            //Address address2 = new Address() { AddressName = "Karmelicka 27/8", Country = "Poland" };

            //_context.Customers.Add(new Customer() { Name = "Hubert Strumiński", Email = "hubert.struminski@microsoft.wsei.edu.pl", Address = address1, PhoneNumber = "+48500034440", Nip = "76543128", Website = "www.divelog.eu" });
            //_context.Customers.Add(new Customer() { Name = "Robert Kazimierz", Email = "robert@wp.pl", Address = address2, PhoneNumber = "+48524014581", Nip = "85765434", Website = "www.google.com" });

            //_context.SaveChanges();
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
    }
}

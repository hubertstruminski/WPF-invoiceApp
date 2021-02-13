using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using WPF_invoiceApp.template;

using Microsoft.EntityFrameworkCore;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public MainWindow()
        {
            InitializeComponent();

            _context.Database.EnsureCreated();
            _context.Addresses.Load();
            _context.Taxes.Load();
        }

        private void OnMyCompanyClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(new CompanyWindow());
        }

        private void OnCustomersClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(new CustomerWindow());
        }

        private void OnInvoicesClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(new InvoiceWindow());
        }

        private void OnProductsClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(new ProductWindow());
        }

        private void OnTaxClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(new TaxWindow());
        }

        private void OnNewTaxWindowClick(object sender, RoutedEventArgs e)
        {
            NewTaxWindow newTaxWindow = new NewTaxWindow();
            newTaxWindow.ShowDialog();
        }

        private void OnNewCompanyClick(object sender, RoutedEventArgs e)
        {
            NewCompanyWindow newCompanyWindow = new NewCompanyWindow();
            newCompanyWindow.ShowDialog();
        }

        private void OnNewCustomerWindowClick(object sender, RoutedEventArgs e)
        {
            NewCustomerWindow newCustomerWindow = new NewCustomerWindow();
            newCustomerWindow.ShowDialog();
        }

        private void OnNewProductWindowClick(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow();
            newProductWindow.ShowDialog();
        }
        
        private void OnNewInvoiceWindowClick(object sender, RoutedEventArgs e)
        {
            NewInvoiceWindow newInvoiceWindow = new NewInvoiceWindow();
            newInvoiceWindow.ShowDialog();
        }
    }
}

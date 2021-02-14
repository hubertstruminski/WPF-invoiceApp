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

        private CompanyWindow companyWindow;
        private CustomerWindow customerWindow;
        private InvoiceWindow invoiceWindow;
        private ProductWindow productWindow;
        private TaxWindow taxWindow;

        public MainWindow()
        {
            InitializeComponent();
            _context.Database.EnsureCreated();

            companyWindow = new CompanyWindow();
            customerWindow = new CustomerWindow();
            invoiceWindow = new InvoiceWindow();
            productWindow = new ProductWindow();
            taxWindow = new TaxWindow();
        }

        private void OnMyCompanyClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(companyWindow);
        }

        private void OnCustomersClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(customerWindow);
        }

        private void OnInvoicesClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(invoiceWindow);
        }

        private void OnProductsClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(productWindow);
        }

        private void OnTaxClick(object sender, RoutedEventArgs e)
        {
            RightViewBox.Children.Clear();
            RightViewBox.Children.Add(taxWindow);
        }

        private void OnNewTaxWindowClick(object sender, RoutedEventArgs e)
        {
            NewTaxWindow newTaxWindow = new NewTaxWindow(taxWindow);
            newTaxWindow.ShowDialog();
        }

        private void OnNewCompanyClick(object sender, RoutedEventArgs e)
        {
            NewCompanyWindow newCompanyWindow = new NewCompanyWindow(companyWindow);
            newCompanyWindow.ShowDialog();
        }

        private void OnNewCustomerWindowClick(object sender, RoutedEventArgs e)
        {
            NewCustomerWindow newCustomerWindow = new NewCustomerWindow(customerWindow);
            newCustomerWindow.ShowDialog();
        }

        private void OnNewProductWindowClick(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow(productWindow);
            newProductWindow.ShowDialog();
        }
        
        private void OnNewInvoiceWindowClick(object sender, RoutedEventArgs e)
        {
            NewInvoiceWindow newInvoiceWindow = new NewInvoiceWindow(invoiceWindow);
            newInvoiceWindow.ShowDialog();
        }
    }
}

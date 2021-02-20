using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext _context;

        private readonly CompanyWindow companyWindow;
        private readonly CustomerWindow customerWindow;
        private readonly InvoiceWindow invoiceWindow;
        private readonly ProductWindow productWindow;
        private readonly TaxWindow taxWindow;

        public MainWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            companyWindow = new CompanyWindow(_context);
            customerWindow = new CustomerWindow(_context);
            invoiceWindow = new InvoiceWindow(_context);
            productWindow = new ProductWindow(_context);
            taxWindow = new TaxWindow(_context);
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
            NewTaxWindow newTaxWindow = new NewTaxWindow(taxWindow, _context);
            newTaxWindow.ShowDialog();
        }

        private void OnNewCompanyClick(object sender, RoutedEventArgs e)
        {
            NewCompanyWindow newCompanyWindow = new NewCompanyWindow(companyWindow, _context);
            newCompanyWindow.ShowDialog();
        }

        private void OnNewCustomerWindowClick(object sender, RoutedEventArgs e)
        {
            NewCustomerWindow newCustomerWindow = new NewCustomerWindow(customerWindow, _context);
            newCustomerWindow.ShowDialog();
        }

        private void OnNewProductWindowClick(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow(productWindow, _context);
            newProductWindow.ShowDialog();
        }
        
        private void OnNewInvoiceWindowClick(object sender, RoutedEventArgs e)
        {
            NewInvoiceWindow newInvoiceWindow = new NewInvoiceWindow(invoiceWindow, _context);
            newInvoiceWindow.ShowDialog();
        }
    }
}

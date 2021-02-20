using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.service;
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

        private readonly MainWindowService service = new MainWindowService();

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
            service.OnSubViewStartup(RightViewBox, companyWindow);
        }

        private void OnCustomersClick(object sender, RoutedEventArgs e)
        {
            service.OnSubViewStartup(RightViewBox, customerWindow);
        }

        private void OnInvoicesClick(object sender, RoutedEventArgs e)
        {
            service.OnSubViewStartup(RightViewBox, invoiceWindow);
        }

        private void OnProductsClick(object sender, RoutedEventArgs e)
        {
            service.OnSubViewStartup(RightViewBox, productWindow);
        }

        private void OnTaxClick(object sender, RoutedEventArgs e)
        {
            service.OnSubViewStartup(RightViewBox, taxWindow);
        }

        private void OnNewTaxWindowClick(object sender, RoutedEventArgs e)
        {
            service.OnNewWindowStartup(typeof(NewTaxWindow), taxWindow, _context);
        }

        private void OnNewCompanyClick(object sender, RoutedEventArgs e)
        {
            service.OnNewWindowStartup(typeof(NewCompanyWindow), companyWindow, _context);
        }

        private void OnNewCustomerWindowClick(object sender, RoutedEventArgs e)
        {
            service.OnNewWindowStartup(typeof(NewCustomerWindow), customerWindow, _context);
        }

        private void OnNewProductWindowClick(object sender, RoutedEventArgs e)
        {
            service.OnNewWindowStartup(typeof(NewProductWindow), productWindow, _context);
        }
        
        private void OnNewInvoiceWindowClick(object sender, RoutedEventArgs e)
        {
            service.OnNewWindowStartup(typeof(NewInvoiceWindow), invoiceWindow, _context);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template;
using WPF_invoiceApp.template.dashboards;
using WPF_invoiceApp.template.lists;

namespace WPF_invoiceApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlite("Data Source = invoiceApp.db");
            });

            services.AddSingleton<MainWindow>();

            services.AddSingleton<CompanyWindow>();
            services.AddSingleton<CustomerWindow>();
            services.AddSingleton<InvoiceWindow>();
            services.AddSingleton<TaxWindow>();
            services.AddSingleton<ProductWindow>();

            services.AddSingleton<NewAddressWindow>();
            services.AddSingleton<NewCompanyWindow>();
            services.AddSingleton<NewCustomerWindow>();
            services.AddSingleton<NewInvoiceWindow>();
            services.AddSingleton<NewProductWindow>();
            services.AddSingleton<NewTaxWindow>();

            services.AddSingleton<ChooseCustomerWindow>();
            services.AddSingleton<ChooseProductWindow>();
            services.AddSingleton<ChooseTaxWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}

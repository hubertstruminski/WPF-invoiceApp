using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource productViewSource;

        private readonly ProductRepository repository = new ProductRepository();
        private readonly InvoiceProductsRepository invoiceProductsRepository = new InvoiceProductsRepository();
        private readonly ProductService service = new ProductService();

        public ProductWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            productViewSource = (CollectionViewSource) FindResource(nameof(productViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Products.Load();
            productViewSource.Source = _context.Products.Local.ToObservableCollection();
        }

        private void ProductDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Product selectedItem = (Product) productDataGrid.SelectedItem;

            _context.Products.Load();
            repository.RemoveProduct(selectedItem, _context);
            
            RefreshProductGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            _context.Taxes.Load();
            Product selectedItem = (Product) productDataGrid.SelectedItem;

            NewProductWindow newProductWindow = new NewProductWindow(selectedItem, this, _context);
            newProductWindow.ShowDialog();
        }

        public void RefreshProductGridData()
        {
            _context.Customers.Load();
            productDataGrid.ItemsSource = null;
            DbSet<Product> products = _context.Products;
            productDataGrid.Items.Clear();
            foreach (Product x in products)
            {
                productDataGrid.Items.Add(x);
            }
            productDataGrid.Items.Refresh();
        }

        private void Button_Show_Click(object sender, RoutedEventArgs e)
        {
            Product selectedItem = (Product) productDataGrid.SelectedItem;

            Product foundProduct = repository.FindByIdWithTaxAndInvoiceProducts(selectedItem, _context);
            foundProduct.InvoiceProducts = invoiceProductsRepository.FindInvoiceProductsWithProductAndInvoiceByProductId(selectedItem, _context);

            ProductDetailsWindow productDetailsWindow = new ProductDetailsWindow(foundProduct);
            service.OnSubViewDetailsShow(productDetailsWindow, RightViewBox);
        }
    }
}

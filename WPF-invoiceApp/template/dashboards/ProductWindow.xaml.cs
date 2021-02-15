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
    /// Logika interakcji dla klasy ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : UserControl
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private CollectionViewSource productViewSource;

        public ProductWindow()
        {
            InitializeComponent();
            productViewSource = (CollectionViewSource) FindResource(nameof(productViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            DbSet<Product> products = _context.Products;
            foreach (Product x in products)
            {
                _context.Products.Remove(x);
            }

            _context.SaveChanges();
            _context.Products.Load();

            productViewSource.Source = _context.Products.Local.ToObservableCollection();

            DbSet<Tax> taxes = _context.Taxes;
            Tax foundTax = null;
            foreach(Tax x in taxes)
            {
                foundTax = x;
            }

            _context.Products.Add(new Product()
            {
                Name = "Computer",
                Description = "desktop",
                Price = 1200,
                Amount = 1,
                Discount = 0,
                Unit = "UNIT",
                Tax = foundTax
            });
            _context.Products.Add(new Product() {  
                Name = "Smartphone",
                Description = "mobile",
                Price = 650,
                Amount = 3,
                Discount = 5,
                Unit = "UNIT",
                Tax = foundTax
            });

            _context.SaveChanges();
        }

        private void OnSelectItem(object sender, SelectionChangedEventArgs e)
        {
            Product selectedItem = (Product) productDataGrid.SelectedItem;
        }

        private void ProductDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Product selectedItem = (Product) productDataGrid.SelectedItem;

            _context.Products.Load();

            _context.Products.Remove(selectedItem);
            _context.SaveChanges();

            // MUST BE FOR REFRESH COUNTER COLUMN AFTER PERFORM DELETE ACTION
            RefreshProductGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            Product selectedItem = (Product) productDataGrid.SelectedItem;

            NewProductWindow newProductWindow = new NewProductWindow(selectedItem, this);
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
    }
}

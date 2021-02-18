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
using System.Windows.Shapes;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template.lists
{
    /// <summary>
    /// Logika interakcji dla klasy ChooseProductWindow.xaml
    /// </summary>
    public partial class ChooseProductWindow : Window
    {
        private DatabaseContext _context;
        private CollectionViewSource productViewSource;

        private NewInvoiceWindow newInvoiceWindow;

        public ChooseProductWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            productViewSource = (CollectionViewSource)FindResource(nameof(productViewSource));
        }

        public ChooseProductWindow(NewInvoiceWindow newInvoiceWindow, DatabaseContext context) : this(context)
        {
            this.newInvoiceWindow = newInvoiceWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            _context.Taxes.Load();
            _context.Products.Load();

            productViewSource.Source = _context.Products.Local.ToObservableCollection();
        }

        private void ProductDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        public void RefreshProductGridData()
        {
            _context.Taxes.Load();
            _context.Products.Load();

            productDataGrid.ItemsSource = null;
            productDataGrid.Items.Clear();

            DbSet<Product> products = _context.Products;
            foreach (Product x in products)
            {
                productDataGrid.Items.Add(x);
            }
            productDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Product selectedItem = (Product)productDataGrid.SelectedItem;
            newInvoiceWindow.SetProduct(selectedItem);
            this.Close();
        }
    }
}

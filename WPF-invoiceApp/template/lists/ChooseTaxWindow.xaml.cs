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
    /// Logika interakcji dla klasy ChooseTaxWindow.xaml
    /// </summary>
    public partial class ChooseTaxWindow : Window
    {
        private DatabaseContext _context;
        private CollectionViewSource taxViewSource;

        private NewProductWindow newProductWindow;

        public ChooseTaxWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            taxViewSource = (CollectionViewSource)FindResource(nameof(taxViewSource));
        }

        public ChooseTaxWindow(NewProductWindow newProductWindow, DatabaseContext context) : this(context)
        {
            this.newProductWindow = newProductWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            //DbSet<Tax> taxes = _context.Taxes;
            //foreach (Tax x in taxes)
            //{
            //    _context.Taxes.Remove(x);
            //}

            //_context.SaveChanges();

            _context.Taxes.Load();
            taxViewSource.Source = _context.Taxes.Local.ToObservableCollection();

            //_context.Taxes.Add(new Tax() { Name = "VAT", Description = "Podatek VAT", TaxAmount = "23%" });
            //_context.Taxes.Add(new Tax() { Name = "PIT", Description = "Podatek PIT", TaxAmount = "11%" });

            //_context.SaveChanges();
        }

        private void TaxDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        public void RefreshTaxGridData()
        {
            _context.Taxes.Load();
            taxDataGrid.ItemsSource = null;
            DbSet<Tax> taxes = _context.Taxes;
            foreach (Tax x in taxes)
            {
                taxDataGrid.Items.Add(x);
            }
            taxDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tax selectedItem = (Tax) taxDataGrid.SelectedItem;
            newProductWindow.SetTax(selectedItem);
            this.Close();
        }
    }
}

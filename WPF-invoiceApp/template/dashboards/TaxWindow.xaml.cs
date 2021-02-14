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
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using System.Windows.Controls.Primitives;

namespace WPF_invoiceApp.template.dashboards
{
    /// <summary>
    /// Logika interakcji dla klasy TaxWindow.xaml
    /// </summary>
    public partial class TaxWindow : UserControl
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private CollectionViewSource taxViewSource;

        public TaxWindow()
        {
            InitializeComponent();
            taxViewSource = (CollectionViewSource)FindResource(nameof(taxViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            DbSet<Tax> taxes = _context.Taxes;
            foreach (Tax x in taxes)
            {
                _context.Taxes.Remove(x);
            }

            _context.SaveChanges();

            _context.Taxes.Load();

            taxViewSource.Source = _context.Taxes.Local.ToObservableCollection();

            _context.Taxes.Add(new Tax() { Name = "VAT", Description = "Podatek VAT", TaxAmount = "23%" });
            _context.Taxes.Add(new Tax() { Name = "PIT", Description = "Podatek PIT", TaxAmount = "11%" });

            _context.SaveChanges();
        }

        private void OnSelectItem(object sender, SelectionChangedEventArgs e)
        {
            Tax selectedItem = (Tax) taxDataGrid.SelectedItem;
        }

        private void TaxDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Tax selectedItem = (Tax) taxDataGrid.SelectedItem;

            _context.Taxes.Load();

            _context.Taxes.Remove(selectedItem);
            _context.SaveChanges();

            // MUST BE FOR REFRESH COUNTER COLUMN AFTER PERFORM DELETE ACTION
            RefreshTaxGridData();

        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            Tax selectedItem = (Tax) taxDataGrid.SelectedItem;

            NewTaxWindow newTaxWindow = new NewTaxWindow(selectedItem, _context, this);
            newTaxWindow.ShowDialog();
        }

        public void RefreshTaxGridData()
        {
            _context.Taxes.Load();
            taxDataGrid.ItemsSource = null;
            DbSet<Tax> taxes = _context.Taxes;
            foreach(Tax x in taxes)
            {
                taxDataGrid.Items.Add(x);
            }
            taxDataGrid.Items.Refresh();
        }
    }
}

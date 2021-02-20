using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template.lists
{
    /// <summary>
    /// Logika interakcji dla klasy ChooseTaxWindow.xaml
    /// </summary>
    public partial class ChooseTaxWindow : Window
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource taxViewSource;

        private readonly NewProductWindow newProductWindow;

        public ChooseTaxWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            taxViewSource = (CollectionViewSource) FindResource(nameof(taxViewSource));
        }

        public ChooseTaxWindow(NewProductWindow newProductWindow, DatabaseContext context) : this(context)
        {
            this.newProductWindow = newProductWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            _context.Taxes.Load();
            taxViewSource.Source = _context.Taxes.Local.ToObservableCollection();
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

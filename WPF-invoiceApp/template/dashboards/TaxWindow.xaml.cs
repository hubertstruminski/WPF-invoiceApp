using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_invoiceApp.context;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using WPF_invoiceApp.repository;

namespace WPF_invoiceApp.template.dashboards
{
    /// <summary>
    /// Logika interakcji dla klasy TaxWindow.xaml
    /// </summary>
    public partial class TaxWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource taxViewSource;

        private readonly TaxRepository repository = new TaxRepository();

        public TaxWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            taxViewSource = (CollectionViewSource) FindResource(nameof(taxViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Taxes.Load();
            taxViewSource.Source = _context.Taxes.Local.ToObservableCollection();
        }

        private void TaxDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Tax selectedItem = (Tax) taxDataGrid.SelectedItem;
            _context.Taxes.Load();

            _context.Entry(selectedItem).State = EntityState.Detached;
            repository.RemoveTax(selectedItem, _context);

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
            taxDataGrid.ItemsSource = null;
            taxDataGrid.Items.Clear();
            DbSet<Tax> taxes = _context.Taxes;
            foreach(Tax x in taxes)
            {
                taxDataGrid.Items.Add(x);
            }
            taxDataGrid.Items.Refresh();
        }
    }
}

using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.template.dashboards
{
    /// <summary>
    /// Logika interakcji dla klasy CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly CollectionViewSource companyViewSource;

        public CompanyWindow(DatabaseContext context)
        {
            _context = context;
            InitializeComponent();
            companyViewSource = (CollectionViewSource)FindResource(nameof(companyViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();

            _context.Companies.Load();

            companyViewSource.Source = _context.Companies.Local.ToObservableCollection();
        }

        private void CompanyDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Company selectedItem = (Company)companyDataGrid.SelectedItem;

            _context.Companies.Load();

            _context.Companies.Remove(selectedItem);
            _context.SaveChanges();

            RefreshCompanyGridData();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            Company selectedItem = (Company)companyDataGrid.SelectedItem;

            NewCompanyWindow newCompanyWindow = new NewCompanyWindow(selectedItem, _context, this);
            newCompanyWindow.ShowDialog();
        }

        public void RefreshCompanyGridData()
        {
            _context.Companies.Load();
            companyDataGrid.ItemsSource = null;
            DbSet<Company> companies = _context.Companies;
            companyDataGrid.Items.Clear();
            foreach (Company x in companies)
            {
                companyDataGrid.Items.Add(x);
            }
            companyDataGrid.Items.Refresh();
        }

        private void Button_Show_Click(object sender, RoutedEventArgs e)
        {
            Company selectedItem = (Company)companyDataGrid.SelectedItem;

            CompanyDetailsWindow companyDetailsWindow = new CompanyDetailsWindow(selectedItem);

            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(companyDetailsWindow);
        }
    }
}

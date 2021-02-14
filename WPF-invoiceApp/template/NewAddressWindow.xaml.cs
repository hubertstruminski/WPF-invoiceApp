using System.Windows;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewAddressWindow.xaml
    /// </summary>
    public partial class NewAddressWindow : Window
    {
        private const string ADDRESS_TEXT = "e.g. Obrońców Warszawy 34/12, Warsaw";
        private const string COUNTRY_TEXT = "Poland";

        private DatabaseContext context = new DatabaseContext();
        private bool isUpdateFlag = false;

        public NewAddressWindow()
        {
            InitializeComponent();
            AssignPlaceholderHandlers();
            context.Database.EnsureCreated();
        }

        //public NewTaxWindow(Tax tax, DatabaseContext context, TaxWindow taxWindow) : this()
        //{
        //    nameTextField.Text = tax.Name;
        //    descriptionTextField.Text = tax.Description;
        //    this.tax = tax;
        //    isUpdateFlag = true;
        //    //context.Dispose();
        //    this.context = context;
        //    this.taxWindow = taxWindow;
        //}

        private void AssignPlaceholderHandlers()
        {
            addressTextField.GotFocus += AddressTextField_GotFocus;
            addressTextField.LostFocus += AddressTextField_LostFocus;

            countryTextField.GotFocus += CountryTextField_GotFocus;
            countryTextField.LostFocus += CountryTextField_LostFocus;
        }

        private void CountryTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(countryTextField.Text))
            {
                countryTextField.Text = COUNTRY_TEXT;
            }
        }

        private void CountryTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(countryTextField.Text.Equals(COUNTRY_TEXT))
            {
                countryTextField.Text = string.Empty;
            }
        }

        private void AddressTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addressTextField.Text))
            {
                addressTextField.Text = ADDRESS_TEXT;
            }
        }

        private void AddressTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(addressTextField.Text.Equals(ADDRESS_TEXT))
            {
                addressTextField.Text = string.Empty;
            }
        }

        private void NewAddressSaveButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

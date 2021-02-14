using ClassLibrary;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewAddressWindow.xaml
    /// </summary>
    public partial class NewAddressWindow : Window
    {
        private const string ADDRESS_TEXT = "e.g. Obrońców Warszawy 34/12, Warsaw";
        private const string COUNTRY_TEXT = "Poland";

        private CustomerWindow customerWindow;
        private NewCustomerWindow newCustomerWindow;
        private Button newAddressButton;
        private Address address;
        private bool isUpdateFlag = false;

        public NewAddressWindow()
        {
            InitializeComponent();
            AssignPlaceholderHandlers();
        }

        public NewAddressWindow(Address address, Button newAddressButton, 
            CustomerWindow customerWindow, NewCustomerWindow newCustomerWindow) : this()
        {
            this.address = address;
            this.newAddressButton = newAddressButton;
            this.customerWindow = customerWindow;
            this.newCustomerWindow = newCustomerWindow;
        }

        public NewAddressWindow(Address address, 
            CustomerWindow customerWindow, Button newAddressButton, NewCustomerWindow newCustomerWindow) : this()
        {
            addressTextField.Text = address.AddressName;
            countryTextField.Text = address.Country;

            this.newCustomerWindow = newCustomerWindow;
            this.customerWindow = customerWindow;
            this.newAddressButton = newAddressButton;
            this.address = address;
            isUpdateFlag = true;
        }

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
            if (!isUpdateFlag)
                address = new Address();

            bool isAddressEmpty = false;
            bool isAddressError = false;

            bool isCountryError = false;

            if (new Regex(ADDRESS_TEXT).IsMatch(addressTextField.Text))
                isAddressEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(addressTextField.Text))
                    isAddressError = true;
            }

            if (!new Regex(".{0,255}").IsMatch(addressTextField.Text))
                isCountryError = true;

            if (isAddressEmpty)
            {
                addressErrorLabel.Visibility = Visibility.Visible;
                addressErrorLabel.Content = "Address field is required.";
            }
            else
            {
                if (isAddressError)
                {
                    addressErrorLabel.Visibility = Visibility.Visible;
                    addressErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    addressErrorLabel.Content = "";
            }

            if (isCountryError)
            {
                countryErrorLabel.Visibility = Visibility.Visible;
                countryErrorLabel.Content = "Maximum length is to 255 characters.";
            }
            else
                countryErrorLabel.Content = "";


            if (isUpdateFlag)
            {
                isAddressEmpty = false;
            }

            if (!isAddressEmpty && !isAddressError && !isCountryError) 
            {
                if (!isUpdateFlag)
                    address = new Address();

                address.AddressName = addressTextField.Text;
                address.Country = countryTextField.Text;

                newAddressButton.Content = addressTextField.Text + ", " + countryTextField.Text;

                newCustomerWindow.SetAddress(address);
                this.Close();
            }
        }
    }
}

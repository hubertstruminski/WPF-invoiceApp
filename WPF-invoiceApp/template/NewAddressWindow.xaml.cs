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
        private CustomerWindow customerWindow;
        private NewCustomerWindow newCustomerWindow;
        private Button newAddressButton;
        private Address address;
        private bool isUpdateFlag = false;

        public NewAddressWindow()
        {
            InitializeComponent();
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

        private void NewAddressSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdateFlag)
                address = new Address();

            bool isAddressEmpty = false;
            bool isAddressError = false;

            bool isCountryError = false;

            if (addressTextField.Text.Length == 0)
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
                addressErrorLabel.Content = "Address is required.";
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

using ClassLibrary;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.service;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewAddressWindow.xaml
    /// </summary>
    public partial class NewAddressWindow : Window
    {
        private readonly AddressService service = new AddressService();

        private readonly CustomerWindow customerWindow;
        private readonly NewCustomerWindow newCustomerWindow;
        private readonly Button newAddressButton;
        private readonly bool isUpdateFlag = false;

        private Address address;

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

            (bool isAddressEmpty, bool isAddressError) = service.ValidateAddressTextField(addressTextField, addressErrorLabel);
            bool isCountryError = service.ValidateCountryTextField(countryTextField, countryErrorLabel);

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

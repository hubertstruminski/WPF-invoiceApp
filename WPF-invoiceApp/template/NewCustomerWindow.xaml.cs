using ClassLibrary;
using System.Windows;
using WPF_invoiceApp.context;
using WPF_invoiceApp.repository;
using WPF_invoiceApp.service;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        private readonly CustomerWindow customerWindow;
        private readonly DatabaseContext context;
        private readonly bool isUpdateFlag = false;

        private Customer customer;
        private Address address;

        private readonly CustomerService service = new CustomerService();
        private readonly CustomerRepository repository = new CustomerRepository();

        public NewCustomerWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
            address = new Address();
        }

        public NewCustomerWindow(CustomerWindow customerWindow, DatabaseContext context) : this(context)
        {
            this.customerWindow = customerWindow;
        }

        public NewCustomerWindow(Customer customer, DatabaseContext context, CustomerWindow customerWindow) : this(context)
        {
            nameTextField.Text = customer.Name;
            emailTextField.Text = customer.Email;
            newAddressButton.Content = customer.Address.AddressName + ", " + customer.Address.Country;
            phoneTextField.Text = customer.PhoneNumber;
            websiteTextField.Text = customer.Website;
            nipTextField.Text = customer.Nip;
            noteTextField.Text = customer.Note;

            address = customer.Address;

            this.customer = customer;
            isUpdateFlag = true;
            this.customerWindow = customerWindow;
        }

        private void OnNewCustomerSaveButtonAction(object sender, RoutedEventArgs e)
        {
            (bool isNameEmpty, bool isNameError) = service.ValidateNameTextField(nameTextField, nameErrorLabel);
            (bool isEmailEmpty, bool isEmailError) = service.ValidateEmailTextField(emailTextField, emailErrorLabel);
            bool isNewAddressEmpty = service.ValidateNewAddressButton(newAddressButton, addressErrorLabel);
            (bool isNipEmpty, bool isNipError) = service.ValidateNipTextField(nipTextField, nipErrorLabel);

            if (isUpdateFlag)
            {
                isNameEmpty = false;
                isEmailEmpty = false;
                isNewAddressEmpty = false;
                isNipEmpty = false;
            }

            if (!isNameEmpty && !isNameError && !isEmailEmpty && 
                !isEmailError && !isNewAddressEmpty && !isNipEmpty && 
                !isNipError)
            {
                if (!isUpdateFlag)
                    customer = new Customer();

                customer.Name = nameTextField.Text;
                customer.Email = emailTextField.Text;
                customer.Address = address;
                customer.PhoneNumber = phoneTextField.Text;
                customer.Website = websiteTextField.Text;
                customer.Nip = nipTextField.Text;
                customer.Note = noteTextField.Text;

                repository.AddCustomer(customer, context, isUpdateFlag);
                customerWindow.RefreshCustomerGridData();
                Close();
            }
        }

        public void SetAddress(Address address)
        {
            this.address = address;
        }

        private void NewAddressButton_Click(object sender, RoutedEventArgs e)
        {
            NewAddressWindow newAddressWindow;

            if (newAddressButton.Content.Equals(">"))
                newAddressWindow = new NewAddressWindow(address, newAddressButton, customerWindow, this);
            else
                newAddressWindow = new NewAddressWindow(address, customerWindow, newAddressButton, this);
            
            newAddressWindow.ShowDialog();
        }
    }
}

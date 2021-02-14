using ClassLibrary;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_invoiceApp.context;
using WPF_invoiceApp.template.dashboards;

namespace WPF_invoiceApp.template
{
    /// <summary>
    /// Logika interakcji dla klasy NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        private const string WEBSITE_TEXT = "www.divelog.eu";
        private const string PHONE_TEXT = "+48 12 21 34 56";
        private const string EMAIL_TEXT = "hubert.struminski@microsoft.wsei.edu.pl";
        private const string NAME_TEXT = "Hubert Strumiński";

        private CustomerWindow customerWindow;
        private DatabaseContext context = new DatabaseContext();
        private Customer customer;
        private Address address;
        private bool isUpdateFlag = false;

        public NewCustomerWindow()
        {
            InitializeComponent();
            AssignPlaceholderHandlers();
            context.Database.EnsureCreated();
            address = new Address();
        }

        public NewCustomerWindow(CustomerWindow customerWindow) : this()
        {
            this.customerWindow = customerWindow;
        }

        public NewCustomerWindow(Customer customer, DatabaseContext context, CustomerWindow customerWindow) : this()
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
            this.context = context;
            this.customerWindow = customerWindow;
        }

        private void AssignPlaceholderHandlers()
        {
            nameTextField.GotFocus += NameTextField_GotFocus;
            nameTextField.LostFocus += NameTextField_LostFocus;

            emailTextField.GotFocus += EmailTextField_GotFocus;
            emailTextField.LostFocus += EmailTextField_LostFocus;

            phoneTextField.GotFocus += PhoneTextField_GotFocus;
            phoneTextField.LostFocus += PhoneTextField_LostFocus;

            websiteTextField.GotFocus += WebsiteTextField_GotFocus;
            websiteTextField.LostFocus += WebsiteTextField_LostFocus;
        }

        private void WebsiteTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(websiteTextField.Text))
            {
                websiteTextField.Text = WEBSITE_TEXT;
            }
        }

        private void WebsiteTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (websiteTextField.Text.Equals(WEBSITE_TEXT))
            {
                websiteTextField.Text = string.Empty;
            }
        }

        private void PhoneTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneTextField.Text))
            {
                phoneTextField.Text = PHONE_TEXT;
            }
        }

        private void PhoneTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (phoneTextField.Text.Equals(PHONE_TEXT))
            {
                phoneTextField.Text = string.Empty;
            }
        }

        private void EmailTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailTextField.Text))
            {
                emailTextField.Text = EMAIL_TEXT;
            }
        }

        private void EmailTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (emailTextField.Text.Equals(EMAIL_TEXT))
            {
                emailTextField.Text = string.Empty;
            }
        }

        private void NameTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(nameTextField.Text))
            {
                nameTextField.Text = NAME_TEXT;
            }
        }

        private void NameTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(nameTextField.Text.Equals(NAME_TEXT))
            {
                nameTextField.Text = string.Empty;
            }
        }

        private void OnNewCustomerSaveButtonAction(object sender, RoutedEventArgs e)
        {
            bool isNameEmpty = false;
            bool isNameError = false;

            bool isEmailEmpty = false;
            bool isEmailError = false;

            bool isNewAddressEmpty = false;

            bool isNipEmpty = false;
            bool isNipError = false;

            if (new Regex(NAME_TEXT).IsMatch(nameTextField.Text))
                isNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(nameTextField.Text))
                    isNameError = true;
            }

            if (new Regex(EMAIL_TEXT).IsMatch(emailTextField.Text))
                isEmailEmpty = true;
            else
            {
                if(!new Regex(".{1,255}").IsMatch(emailTextField.Text))
                    isEmailError = true;
            }

            if (newAddressButton.Content.Equals(">"))
                isNewAddressEmpty = true;

            if (nipTextField.Text.Length == 0)
                isNipEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(emailTextField.Text))
                    isNipError = true;
            }

            if (isNameEmpty)
            {
                nameErrorLabel.Visibility = Visibility.Visible;
                nameErrorLabel.Content = "Name field is required.";
            }
            else
            {
                if (isNameError)
                {
                    nameErrorLabel.Visibility = Visibility.Visible;
                    nameErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    nameErrorLabel.Content = "";
            }

            if (isEmailEmpty)
            {
                emailErrorLabel.Visibility = Visibility.Visible;
                emailErrorLabel.Content = "Email field is required.";
            }
            else
            {
                if (isEmailError)
                {
                    emailErrorLabel.Visibility = Visibility.Visible;
                    emailErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    emailErrorLabel.Content = "";
            }

            if (isNewAddressEmpty)
            {
                addressErrorLabel.Visibility = Visibility.Visible;
                addressErrorLabel.Content = "Address field is required.";
            }
            else
            {
                addressErrorLabel.Content = "";
            }

            if (isNipEmpty)
            {
                nipErrorLabel.Visibility = Visibility.Visible;
                nipErrorLabel.Content = "NIP field is required.";
            }
            else
            {
                if (isNipError)
                {
                    nipErrorLabel.Visibility = Visibility.Visible;
                    nipErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    nipErrorLabel.Content = "";
            }

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

                if (isUpdateFlag)
                    context.Customers.Update(customer);
                else
                    context.Customers.Add(customer);

                context.SaveChanges();
                customerWindow.RefreshCustomerGridData();

                this.Close();
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

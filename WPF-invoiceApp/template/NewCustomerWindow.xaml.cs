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
        private CustomerWindow customerWindow;
        private DatabaseContext context;
        private Customer customer;
        private Address address;
        private bool isUpdateFlag = false;

        public NewCustomerWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
            context.Database.EnsureCreated();
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
            bool isNameEmpty = false;
            bool isNameError = false;

            bool isEmailEmpty = false;
            bool isEmailError = false;

            bool isNewAddressEmpty = false;

            bool isNipEmpty = false;
            bool isNipError = false;

            if (nameTextField.Text.Length == 0)
                isNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(nameTextField.Text))
                    isNameError = true;
            }

            if (emailTextField.Text.Length == 0)
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
                nameErrorLabel.Content = "Name is required.";
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
                emailErrorLabel.Content = "Email is required.";
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
                addressErrorLabel.Content = "Address is required.";
            }
            else
            {
                addressErrorLabel.Content = "";
            }

            if (isNipEmpty)
            {
                nipErrorLabel.Visibility = Visibility.Visible;
                nipErrorLabel.Content = "NIP is required.";
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

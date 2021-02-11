using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public NewCustomerWindow()
        {
            InitializeComponent();

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

        }
    }
}

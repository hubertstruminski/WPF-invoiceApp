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
                websiteTextField.Text = "www.divelog.eu";
            }
        }

        private void WebsiteTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (websiteTextField.Text.Equals("www.divelog.eu"))
            {
                websiteTextField.Text = "";
            }
        }

        private void PhoneTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneTextField.Text))
            {
                phoneTextField.Text = "+48 12 21 34 56";
            }
        }

        private void PhoneTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (phoneTextField.Text.Equals("+48 12 21 34 56"))
            {
                phoneTextField.Text = "";
            }
        }

        private void EmailTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailTextField.Text))
            {
                emailTextField.Text = "hubert.struminski@microsoft.wsei.edu.pl";
            }
        }

        private void EmailTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (emailTextField.Text.Equals("hubert.struminski@microsoft.wsei.edu.pl"))
            {
                emailTextField.Text = "";
            }
        }

        private void NameTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(nameTextField.Text))
            {
                nameTextField.Text = "Hubert Strumiński";
            }
        }

        private void NameTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(nameTextField.Text.Equals("Hubert Strumiński"))
            {
                nameTextField.Text = "";
            }
        }

        private void OnNewCustomerSaveButtonAction(object sender, RoutedEventArgs e)
        {

        }
    }
}

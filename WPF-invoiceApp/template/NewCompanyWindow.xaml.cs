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
    /// Logika interakcji dla klasy NewCompanyWindow.xaml
    /// </summary>
    public partial class NewCompanyWindow : Window
    {
        public NewCompanyWindow()
        {
            InitializeComponent();

            companyNameTextField.GotFocus += CompanyNameTextField_GotFocus;
            companyNameTextField.LostFocus += CompanyNameTextField_LostFocus;

            addressTextField.GotFocus += AddressTextField_GotFocus;
            addressTextField.LostFocus += AddressTextField_LostFocus;

            postalcodeTextField.GotFocus += PostalcodeTextField_GotFocus;
            postalcodeTextField.LostFocus += PostalcodeTextField_LostFocus;

            cityTextField.GotFocus += CityTextField_GotFocus;
            cityTextField.LostFocus += CityTextField_LostFocus;

            countryTextField.GotFocus += CountryTextField_GotFocus;
            countryTextField.LostFocus += CountryTextField_LostFocus;
        }

        private void CountryTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(countryTextField.Text))
            {
                countryTextField.Text = "Poland";
            }
        }

        private void CountryTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (countryTextField.Text.Equals("Poland"))
            {
                countryTextField.Text = "";
            }
        }

        private void CityTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cityTextField.Text))
            {
                cityTextField.Text = "Cracow";
            }
        }

        private void CityTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cityTextField.Text.Equals("Cracow"))
            {
                cityTextField.Text = "";
            }
        }

        private void PostalcodeTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(postalcodeTextField.Text))
            {
                postalcodeTextField.Text = "30-054";
            }
        }

        private void PostalcodeTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (postalcodeTextField.Text.Equals("30-054"))
            {
                postalcodeTextField.Text = "";
            }
        }

        private void AddressTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addressTextField.Text))
            {
                addressTextField.Text = "Karmelicka 24/7";
            }
        }

        private void AddressTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (addressTextField.Text.Equals("Karmelicka 24/7"))
            {
                addressTextField.Text = "";
            }
        }

        private void CompanyNameTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(companyNameTextField.Text))
            {
                companyNameTextField.Text = "WSEI";
            }
        }

        private void CompanyNameTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(companyNameTextField.Text.Equals("WSEI"))
            {
                companyNameTextField.Text = "";
            }
        }

        private void OnNewCompanySaveButton(object sender, RoutedEventArgs e)
        {

        }
    }
}

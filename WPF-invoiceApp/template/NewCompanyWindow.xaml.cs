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
        private const string COMPANY_NAME_TEXT = "WSEI";
        private const string ADDRESS_TEXT = "Karmelicka 24/7";
        private const string POSTAL_CODE_TEXT = "30-054";
        private const string CITY_TEXT = "Cracow";
        private const string COUNTRY_TEXT = "Poland";

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
                countryTextField.Text = COUNTRY_TEXT;
            }
        }

        private void CountryTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (countryTextField.Text.Equals(COUNTRY_TEXT))
            {
                countryTextField.Text = string.Empty;
            }
        }

        private void CityTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cityTextField.Text))
            {
                cityTextField.Text = CITY_TEXT;
            }
        }

        private void CityTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cityTextField.Text.Equals(CITY_TEXT))
            {
                cityTextField.Text = string.Empty;
            }
        }

        private void PostalcodeTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(postalcodeTextField.Text))
            {
                postalcodeTextField.Text = POSTAL_CODE_TEXT;
            }
        }

        private void PostalcodeTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (postalcodeTextField.Text.Equals(POSTAL_CODE_TEXT))
            {
                postalcodeTextField.Text = string.Empty;
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
            if (addressTextField.Text.Equals(ADDRESS_TEXT)) { 
                addressTextField.Text = string.Empty;
            }
        }

        private void CompanyNameTextField_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(companyNameTextField.Text))
            {
                companyNameTextField.Text = COMPANY_NAME_TEXT;
            }
        }

        private void CompanyNameTextField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(companyNameTextField.Text.Equals(COMPANY_NAME_TEXT))
            {
                companyNameTextField.Text = string.Empty;
            }
        }

        private void OnNewCompanySaveButton(object sender, RoutedEventArgs e)
        {

        }
    }
}

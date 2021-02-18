using ClassLibrary;
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
    /// Logika interakcji dla klasy NewCompanyWindow.xaml
    /// </summary>
    public partial class NewCompanyWindow : Window
    {
        private const string COMPANY_NAME_TEXT = "WSEI";
        private const string ADDRESS_TEXT = "Karmelicka 24/7";
        private const string POSTAL_CODE_TEXT = "30-054";
        private const string CITY_TEXT = "Cracow";
        private const string COUNTRY_TEXT = "Poland";

        private CompanyWindow companyWindow;
        private DatabaseContext context;
        private Company company;
        private bool isUpdateFlag = false;

        public NewCompanyWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
            AssignPlaceholderHandlers();
            context.Database.EnsureCreated();
        }

        public NewCompanyWindow(CompanyWindow companyWindow, DatabaseContext context) : this(context)
        {
            this.companyWindow = companyWindow;
        }

        public NewCompanyWindow(Company company, DatabaseContext context, CompanyWindow companyWindow) : this(context)
        {
            companyNameTextField.Text = company.CompanyName;
            addressTextField.Text = company.Address;
            cityTextField.Text = company.City;
            postalcodeTextField.Text = company.PostalCode;
            countryTextField.Text = company.Country;

            this.company = company;
            isUpdateFlag = true;
            //context.Dispose();
            this.companyWindow = companyWindow;
        }

        private void AssignPlaceholderHandlers()
        {
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
            bool isCompanyNameEmpty = false;
            bool isCompanyNameError = false;

            bool isAddressEmpty = false;
            bool isAddressError = false;

            bool isPostalCodeEmpty = false;
            bool isPostalCodeError = false;

            bool isCityEmpty = false;
            bool isCityError = false;

            bool isCountryEmpty = false;
            bool isCountryError = false;

            if (new Regex(COMPANY_NAME_TEXT).IsMatch(companyNameTextField.Text))
                isCompanyNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(companyNameTextField.Text))
                    isCompanyNameError = true;
            }

            if (new Regex(ADDRESS_TEXT).IsMatch(addressTextField.Text))
                isAddressEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(addressTextField.Text))
                    isAddressError = true;
            }

            if (new Regex(POSTAL_CODE_TEXT).IsMatch(postalcodeTextField.Text))
                isPostalCodeEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(postalcodeTextField.Text))
                    isPostalCodeError = true;
            }

            if (new Regex(CITY_TEXT).IsMatch(cityTextField.Text))
                isCityEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(cityTextField.Text))
                    isCityError = true;
            }

            if (new Regex(COUNTRY_TEXT).IsMatch(countryTextField.Text))
                isCountryEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(countryTextField.Text))
                    isCountryError = true;
            }

            if (isCompanyNameEmpty)
            {
                companyNameErrorLabel.Visibility = Visibility.Visible;
                companyNameErrorLabel.Content = "Company name field is required.";
            }
            else
            {
                if (isCompanyNameError)
                {
                    companyNameErrorLabel.Visibility = Visibility.Visible;
                    companyNameErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    companyNameErrorLabel.Content = "";
            }

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

            if (isPostalCodeEmpty)
            {
                postalcodeErrorLabel.Visibility = Visibility.Visible;
                postalcodeErrorLabel.Content = "PostalCode field is required.";
            }
            else
            {
                if (isPostalCodeError)
                {
                    postalcodeErrorLabel.Visibility = Visibility.Visible;
                    postalcodeErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    postalcodeErrorLabel.Content = "";
            }

            if (isCityEmpty)
            {
                cityErrorLabel.Visibility = Visibility.Visible;
                cityErrorLabel.Content = "City field is required.";
            }
            else
            {
                if (isCityError)
                {
                    cityErrorLabel.Visibility = Visibility.Visible;
                    cityErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    cityErrorLabel.Content = "";
            }

            if (isCountryEmpty)
            {
                countryErrorLabel.Visibility = Visibility.Visible;
                countryErrorLabel.Content = "Country field is required.";
            }
            else
            {
                if (isCountryError)
                {
                    countryErrorLabel.Visibility = Visibility.Visible;
                    countryErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    countryErrorLabel.Content = "";
            }

            if(isUpdateFlag)
            {
                isCompanyNameEmpty = false;
                isAddressEmpty = false;
                isPostalCodeEmpty = false;
                isCityEmpty = false;
                isCountryEmpty = false;
            }

            if(!isCompanyNameEmpty && !isCompanyNameError && !isAddressEmpty && 
                !isAddressError && !isPostalCodeEmpty && !isPostalCodeError && 
                !isCityEmpty && !isCityError && !isCountryEmpty && !isCountryError)
            {
                if (!isUpdateFlag)
                    company = new Company();

                company.CompanyName = companyNameTextField.Text;
                company.Address = addressTextField.Text;
                company.City = cityTextField.Text;
                company.Country = countryTextField.Text;
                company.PostalCode = postalcodeTextField.Text;

                if (isUpdateFlag)
                    context.Companies.Update(company);
                else
                    context.Companies.Add(company);

                context.SaveChanges();
                companyWindow.RefreshCompanyGridData();
                this.Close();
            }
        }   

    }
}

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
        private CompanyWindow companyWindow;
        private DatabaseContext context;
        private Company company;
        private bool isUpdateFlag = false;

        public NewCompanyWindow(DatabaseContext context)
        {
            this.context = context;
            InitializeComponent();
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
            this.companyWindow = companyWindow;
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

            if (companyNameTextField.Text.Length == 0)
                isCompanyNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(companyNameTextField.Text))
                    isCompanyNameError = true;
            }

            if (addressTextField.Text.Length == 0)
                isAddressEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(addressTextField.Text))
                    isAddressError = true;
            }

            if (postalcodeTextField.Text.Length == 0)
                isPostalCodeEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(postalcodeTextField.Text))
                    isPostalCodeError = true;
            }

            if (cityTextField.Text.Length == 0)
                isCityEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(cityTextField.Text))
                    isCityError = true;
            }

            if (countryTextField.Text.Length == 0)
                isCountryEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(countryTextField.Text))
                    isCountryError = true;
            }

            if (isCompanyNameEmpty)
            {
                companyNameErrorLabel.Visibility = Visibility.Visible;
                companyNameErrorLabel.Content = "Company name is required.";
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

            if (isPostalCodeEmpty)
            {
                postalcodeErrorLabel.Visibility = Visibility.Visible;
                postalcodeErrorLabel.Content = "PostalCode is required.";
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
                cityErrorLabel.Content = "City is required.";
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
                countryErrorLabel.Content = "Country is required.";
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

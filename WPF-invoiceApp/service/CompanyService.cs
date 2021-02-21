using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class CompanyService
    {
        public void OnSubViewDetailsShow(CompanyDetailsWindow companyDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(companyDetailsWindow);
        }

        public (bool, bool) ValidateCompanyNameTextField(TextBox companyNameTextField, Label companyNameErrorLabel)
        {
            bool isCompanyNameEmpty = false;
            bool isCompanyNameError = false;

            if (companyNameTextField.Text.Length == 0)
                isCompanyNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(companyNameTextField.Text))
                    isCompanyNameError = true;
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

            return (isCompanyNameEmpty, isCompanyNameError);
        }

        public (bool, bool) ValidateAddressTextField(TextBox addressTextField, Label addressErrorLabel)
        {
            bool isAddressEmpty = false;
            bool isAddressError = false;

            if (addressTextField.Text.Length == 0)
                isAddressEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(addressTextField.Text))
                    isAddressError = true;
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

            return (isAddressEmpty, isAddressError);
        }

        public (bool, bool) ValidatePostalcodeTextField(TextBox postalcodeTextField, Label postalcodeErrorLabel)
        {
            bool isPostalCodeEmpty = false;
            bool isPostalCodeError = false;

            if (postalcodeTextField.Text.Length == 0)
                isPostalCodeEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(postalcodeTextField.Text))
                    isPostalCodeError = true;
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

            return (isPostalCodeEmpty, isPostalCodeError);
        }

        public (bool, bool) ValidateCityTextField(TextBox cityTextField, Label cityErrorLabel)
        {
            bool isCityEmpty = false;
            bool isCityError = false;

            if (cityTextField.Text.Length == 0)
                isCityEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(cityTextField.Text))
                    isCityError = true;
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

            return (isCityEmpty, isCityError);
        }

        public (bool, bool) ValidateCountryTextField(TextBox countryTextField, Label countryErrorLabel)
        {
            bool isCountryEmpty = false;
            bool isCountryError = false;

            if (countryTextField.Text.Length == 0)
                isCountryEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(countryTextField.Text))
                    isCountryError = true;
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

            return (isCountryEmpty, isCountryError);
        }
    }
}

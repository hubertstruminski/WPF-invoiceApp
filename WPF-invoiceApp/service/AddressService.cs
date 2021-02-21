using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPF_invoiceApp.service
{
    public class AddressService
    {
        /// <summary>
        /// Validate Address text field
        /// </summary>
        /// <param name="addressTextField">TextBox object</param>
        /// <param name="addressErrorLabel">Label object</param>
        /// <returns>Returns two boolean variable which specify whether field is empty or error occurs</returns>
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

        /// <summary>
        /// Validate Country text field
        /// </summary>
        /// <param name="countryTextField">TextBox object</param>
        /// <param name="countryErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether error occurs in field</returns>
        public bool ValidateCountryTextField(TextBox countryTextField, Label countryErrorLabel)
        {
            bool isCountryError = false;

            if (!new Regex(".{0,255}").IsMatch(countryTextField.Text))
                isCountryError = true;

            if (isCountryError)
            {
                countryErrorLabel.Visibility = Visibility.Visible;
                countryErrorLabel.Content = "Maximum length is to 255 characters.";
            }
            else
                countryErrorLabel.Content = "";

            return isCountryError;
        }
    }
}

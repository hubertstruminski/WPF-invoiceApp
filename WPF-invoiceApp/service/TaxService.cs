using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPF_invoiceApp.service
{
    public class TaxService
    {
        public bool ValidateTaxNameTextField(TextBox nameTextField, Label taxNameErrorLabel)
        {
            bool isTaxNameError = false;

            if (!new Regex(".{1,255}").IsMatch(nameTextField.Text))
                isTaxNameError = true;

            if (isTaxNameError)
            {
                taxNameErrorLabel.Visibility = Visibility.Visible;
                taxNameErrorLabel.Content = "Name length must be from 1 to 255 characters.";
            }
            else
                taxNameErrorLabel.Content = "";

            return isTaxNameError;
        }

        public bool ValidateTaxDescriptionTextField(TextBox descriptionTextField, Label taxDescriptionErrorLabel)
        {
            bool isTaxDescriptionError = false;

            if (!new Regex(".{0,255}").IsMatch(descriptionTextField.Text))
                isTaxDescriptionError = true;

            if (isTaxDescriptionError)
            {
                taxDescriptionErrorLabel.Visibility = Visibility.Visible;
                taxDescriptionErrorLabel.Content = "Maximum length is 255 characters.";
            }
            else
                taxDescriptionErrorLabel.Content = "";

            return isTaxDescriptionError;
        }

        public bool ValidateTaxAmountTextField(TextBox taxAmountTextField, Label taxAmountErrorLabel)
        {
            bool isTaxAmountError = false;

            if (!new Regex("[0-9]+%$").IsMatch(taxAmountTextField.Text))
                isTaxAmountError = true;

            if (isTaxAmountError)
            {
                taxAmountErrorLabel.Visibility = Visibility.Visible;
                taxAmountErrorLabel.Content = "Incorrect format, e.g. 23%";
            }
            else
                taxAmountErrorLabel.Content = "";

            return isTaxAmountError;
        }

    }
}

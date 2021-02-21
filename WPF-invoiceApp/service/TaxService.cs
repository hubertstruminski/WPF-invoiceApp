using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPF_invoiceApp.service
{
    public class TaxService
    {
        /// <summary>
        /// Validate Tax Name text field
        /// </summary>
        /// <param name="nameTextField">TextBox object</param>
        /// <param name="taxNameErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether error occurs in field</returns>
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

        /// <summary>
        /// Validate Tax Description text field
        /// </summary>
        /// <param name="descriptionTextField">TextBox object</param>
        /// <param name="taxDescriptionErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether error occurs in field</returns>
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

        /// <summary>
        /// Validate Tax Amount text field
        /// </summary>
        /// <param name="taxAmountTextField">TextBox object</param>
        /// <param name="taxAmountErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether error occurs in field</returns>
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

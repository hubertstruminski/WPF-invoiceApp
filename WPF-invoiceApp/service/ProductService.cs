using ClassLibrary;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class ProductService
    {
        /// <summary>
        /// Shows details Window
        /// </summary>
        /// <param name="productDetailsWindow">ProductDetailsWindow object</param>
        /// <param name="RightViewBox">Grid object where is injected details window</param>
        public void OnSubViewDetailsShow(ProductDetailsWindow productDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(productDetailsWindow);
        }

        /// <summary>
        /// Creates TextBlock with Invoice details
        /// </summary>
        /// <param name="ip">InvoiceProduct object</param>
        /// <param name="count">Number element in sequence</param>
        /// <returns>Returns TextBlock object</returns>
        public TextBlock CreateInvoiceTextBlock(InvoiceProduct ip, int count)
        {
            TextBlock text = new TextBlock();
            text.Text = count + ". Number: " + ip.Invoice.Number + ", Date: " + ip.Invoice.Date + ", Deadline: " + ip.Invoice.Deadline;
            text.FontSize = 16;

            return text;
        }

        /// <summary>
        /// Validate Name text field
        /// </summary>
        /// <param name="nameTextField">TextBox object</param>
        /// <param name="nameErrorLabel">Label object</param>
        /// <returns>Returns two boolean variable which specify whether field is empty or error occurs</returns>
        public (bool, bool) ValidateNameTextField(TextBox nameTextField, Label nameErrorLabel)
        {
            bool isNameEmpty = false;
            bool isNameError = false;

            if (nameTextField.Text.Length == 0)
                isNameEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(nameTextField.Text))
                    isNameError = true;
            }

            if (isNameEmpty)
            {
                nameErrorLabel.Visibility = Visibility.Visible;
                nameErrorLabel.Content = "Name is required.";
            }
            else
            {
                if (isNameError)
                {
                    nameErrorLabel.Visibility = Visibility.Visible;
                    nameErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    nameErrorLabel.Content = "";
            }

            return (isNameEmpty, isNameError);
        }

        /// <summary>
        /// Validate Description text field
        /// </summary>
        /// <param name="descriptionTextField">TextBox object</param>
        /// <param name="descriptionErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether field is empty</returns>
        public bool ValidateDescriptionTextField(TextBox descriptionTextField, Label descriptionErrorLabel)
        {
            bool isDescriptionEmpty = false;

            if (!new Regex(".{0,255}").IsMatch(descriptionTextField.Text))
                isDescriptionEmpty = true;

            if (isDescriptionEmpty)
            {
                descriptionErrorLabel.Visibility = Visibility.Visible;
                descriptionErrorLabel.Content = "Maximum length is to 255 characters.";
            }
            else
                descriptionErrorLabel.Content = "";

            return isDescriptionEmpty;
        }

        /// <summary>
        /// Validate Price text field
        /// </summary>
        /// <param name="priceTextField">TextBox object</param>
        /// <param name="priceErrorLabel">Label object</param>
        /// <returns>Returns two boolean variable which specify whether field is empty or error occurs</returns>
        public (bool, bool) ValidatePriceTextField(TextBox priceTextField, Label priceErrorLabel)
        {
            bool isPriceEmpty = false;
            bool isPriceError = false;

            if (priceTextField.Text.Length == 0)
                isPriceEmpty = true;
            else
            {
                if (!new Regex("\\d+(\\,\\d{1,2})?").IsMatch(priceTextField.Text))
                    isPriceError = true;
            }

            if (isPriceEmpty)
            {
                priceErrorLabel.Visibility = Visibility.Visible;
                priceErrorLabel.Content = "Price is required.";
            }
            else
            {
                if (isPriceError)
                {
                    priceErrorLabel.Visibility = Visibility.Visible;
                    priceErrorLabel.Content = "Value must contains dot or be integer number.";
                }
                else
                    priceErrorLabel.Content = "";
            }

            return (isPriceEmpty, isPriceError);
        }

        /// <summary>
        /// Validate Discount text field
        /// </summary>
        /// <param name="discountTextField">TextBox object</param>
        /// <param name="discountErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether error occurs in field</returns>
        public bool ValidateDiscountTextField(TextBox discountTextField, Label discountErrorLabel)
        {
            bool isDiscountError = false;

            if (discountTextField.Text.Length > 0)
            {
                if (!new Regex("[0-9]{1,3}").IsMatch(discountTextField.Text))
                    isDiscountError = true;
            }

            if (isDiscountError)
            {
                discountErrorLabel.Visibility = Visibility.Visible;
                discountErrorLabel.Content = "Integer number between 0 and 100 is required.";
            }
            else
                discountErrorLabel.Content = "";

            return isDiscountError;
        }

        /// <summary>
        /// Validate Amount text field
        /// </summary>
        /// <param name="amountTextField">TextBox object</param>
        /// <param name="amountErrorLabel">Label object</param>
        /// <returns>Returns two boolean variable which specify whether field is empty or error occurs</returns>
        public (bool, bool) ValidateAmountTextField(TextBox amountTextField, Label amountErrorLabel)
        {
            bool isAmountEmpty = false;
            bool isAmountError = false;

            if (amountTextField.Text.Length == 0)
                isAmountEmpty = true;
            else
            {
                if (!new Regex("\\d+(\\.\\d{1,2})?").IsMatch(amountTextField.Text))
                    isAmountError = true;
            }

            if (isAmountEmpty)
            {
                amountErrorLabel.Visibility = Visibility.Visible;
                amountErrorLabel.Content = "Amount is required.";
            }
            else
            {
                if (isAmountError)
                {
                    amountErrorLabel.Visibility = Visibility.Visible;
                    amountErrorLabel.Content = "Value must contains dot or be integer number.";
                }
                else
                    amountErrorLabel.Content = "";
            }

            return (isAmountEmpty, isAmountError);
        }

        /// <summary>
        /// Validate Unit text field
        /// </summary>
        /// <param name="unitTextField">TextBox object</param>
        /// <param name="unitErrorLabel">Label object</param>
        /// <returns>Returns two boolean variable which specify whether field is empty or error occurs</returns>
        public (bool, bool) ValidateUnitTextField(TextBox unitTextField, Label unitErrorLabel)
        {
            bool isUnitEmpty = false;
            bool isUnitError = false;

            if (unitTextField.Text.Length == 0)
                isUnitEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(unitTextField.Text))
                    isUnitError = true;
            }

            if (isUnitEmpty)
            {
                unitErrorLabel.Visibility = Visibility.Visible;
                unitErrorLabel.Content = "Unit is required.";
            }
            else
            {
                if (isUnitError)
                {
                    unitErrorLabel.Visibility = Visibility.Visible;
                    unitErrorLabel.Content = "Maximum length is form 1 to 255 characters.";
                }
                else
                    unitErrorLabel.Content = "";
            }

            return (isUnitEmpty, isUnitError);
        }

        /// <summary>
        /// Validate AddTax button
        /// </summary>
        /// <param name="addTaxButton">Button object</param>
        /// <param name="taxErrorLabel">Label object</param>
        /// <returns>Returns boolean value which specify whether field is empty</returns>
        public bool ValidateAddTaxButton(Button addTaxButton, Label taxErrorLabel)
        {
            bool isTaxEmpty = false;

            if (addTaxButton.Content.Equals(">"))
                isTaxEmpty = true;

            if (isTaxEmpty)
            {
                taxErrorLabel.Visibility = Visibility.Visible;
                taxErrorLabel.Content = "Tax is required.";
            }
            else
                taxErrorLabel.Content = "";

            return isTaxEmpty;
        }

    }
}

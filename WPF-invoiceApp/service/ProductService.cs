using ClassLibrary;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class ProductService
    {
        public void OnSubViewDetailsShow(ProductDetailsWindow productDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(productDetailsWindow);
        }

        public TextBlock CreateInvoiceTextBlock(InvoiceProduct ip, int count)
        {
            TextBlock text = new TextBlock();
            text.Text = count + ". Number: " + ip.Invoice.Number + ", Date: " + ip.Invoice.Date + ", Deadline: " + ip.Invoice.Deadline;
            text.FontSize = 16;

            return text;
        }

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

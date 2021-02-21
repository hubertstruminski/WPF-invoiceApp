using ClassLibrary;
using System.Windows;
using System.Windows.Controls;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class InvoiceService
    {
        private readonly MainWindowService service = new MainWindowService();

        public void OnSubViewDetailsShow(InvoiceDetailsWindow invoiceDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(invoiceDetailsWindow);
        }

        public StackPanel CreateProductStackPanel(InvoiceProduct ip, int count)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            service.CreateTextBlock(stackPanel, count + ". " + ip.Product.Name, true);
            service.CreateTextBlock(stackPanel, "Amount: " + ip.Product.Amount);
            service.CreateTextBlock(stackPanel, "Price: " + ip.Product.Price + "$");

            return stackPanel;
        }

        public bool ValidateNumberTextField(TextBox numberTextField, Label numberErrorLabel)
        {
            bool isNumberEmpty = false;

            if (numberTextField.Text.Length == 0)
                isNumberEmpty = true;

            if (isNumberEmpty)
            {
                numberErrorLabel.Visibility = Visibility.Visible;
                numberErrorLabel.Content = "Number is required.";
            }
            else
            {
                numberErrorLabel.Content = "";
            }

            return isNumberEmpty;
        }

        public bool ValidateCustomerButton(Button addCustomerButton, Label customerErrorLabel)
        {
            bool isCustomerEmpty = false;

            if (addCustomerButton.Content.Equals(">"))
                isCustomerEmpty = true;

            if (isCustomerEmpty)
            {
                customerErrorLabel.Visibility = Visibility.Visible;
                customerErrorLabel.Content = "Customer is required.";
            }
            else
            {
                customerErrorLabel.Content = "";
            }

            return isCustomerEmpty;
        }

        public bool ValidateProductButton(Button listProductsButton, Label productErrorLabel)
        {
            bool isProductEmpty = false;

            if (listProductsButton.Content.Equals(""))
                isProductEmpty = true;

            if (isProductEmpty)
            {
                productErrorLabel.Visibility = Visibility.Visible;
                productErrorLabel.Content = "Product is required.";
            }
            else
            {
                productErrorLabel.Content = "";
            }

            return isProductEmpty;
        }
    }
}

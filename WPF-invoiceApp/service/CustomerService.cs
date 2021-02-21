using ClassLibrary;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_invoiceApp.template.details;

namespace WPF_invoiceApp.service
{
    public class CustomerService
    {
        private readonly MainWindowService service = new MainWindowService();

        public void OnSubViewDetailsShow(CustomerDetailsWindow customerDetailsWindow, Grid RightViewBox)
        {
            RightViewBox.Children.Clear();

            RightViewBox.VerticalAlignment = VerticalAlignment.Stretch;
            RightViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;

            RightViewBox.Children.Add(customerDetailsWindow);
        }

        public Grid CreateInvoiceGrid(Invoice invoice)
        {
            Grid grid = new Grid();

            ColumnDefinition c1 = new ColumnDefinition();
            ColumnDefinition c2 = new ColumnDefinition();
            ColumnDefinition c3 = new ColumnDefinition();

            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            grid.ColumnDefinitions.Add(c3);

            StackPanel s1c1 = new StackPanel();
            s1c1.Orientation = Orientation.Vertical;

            service.CreateTextBlock(s1c1, "Invoice", true);
            service.CreateTextBlock(s1c1, "Number: " + invoice.Number);
            service.CreateTextBlock(s1c1, "Date: " + invoice.Date);
            service.CreateTextBlock(s1c1, "Deadline: " + invoice.Deadline);

            Grid.SetColumn(s1c1, 0);

            StackPanel s2c2 = new StackPanel();
            s2c2.Orientation = Orientation.Vertical;

            service.CreateTextBlock(s2c2, "Customer", true);
            service.CreateTextBlock(s2c2, "Name: " + invoice.Customer.Name);
            service.CreateTextBlock(s2c2, "Address: " + invoice.Customer.Address.AddressName + " " + invoice.Customer.Address.Country);
            service.CreateTextBlock(s2c2, "Phone number: " + invoice.Customer.PhoneNumber);

            Grid.SetColumn(s2c2, 1);


            StackPanel s3c3 = new StackPanel();
            s3c3.Orientation = Orientation.Vertical;

            service.CreateTextBlock(s3c3, "Status", true);
            TextBlock statusValue = new TextBlock();
            statusValue.Text = invoice.Status.ToString();
            if (invoice.Status == Status.SENT)
                statusValue.Foreground = Brushes.Green;
            else
                statusValue.Foreground = Brushes.Red;
            statusValue.FontWeight = FontWeights.Bold;
            s3c3.Children.Add(statusValue);

            Grid.SetColumn(s3c3, 2);

            grid.Children.Add(s1c1);
            grid.Children.Add(s2c2);
            grid.Children.Add(s3c3);

            return grid;
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

        public (bool, bool) ValidateEmailTextField(TextBox emailTextField, Label emailErrorLabel)
        {
            bool isEmailEmpty = false;
            bool isEmailError = false;

            if (emailTextField.Text.Length == 0)
                isEmailEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(emailTextField.Text))
                    isEmailError = true;
            }

            if (isEmailEmpty)
            {
                emailErrorLabel.Visibility = Visibility.Visible;
                emailErrorLabel.Content = "Email is required.";
            }
            else
            {
                if (isEmailError)
                {
                    emailErrorLabel.Visibility = Visibility.Visible;
                    emailErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    emailErrorLabel.Content = "";
            }

            return (isEmailEmpty, isEmailError);
        }

        public bool ValidateNewAddressButton(Button newAddressButton, Label addressErrorLabel)
        {
            bool isNewAddressEmpty = false;

            if (newAddressButton.Content.Equals(">"))
                isNewAddressEmpty = true;

            if (isNewAddressEmpty)
            {
                addressErrorLabel.Visibility = Visibility.Visible;
                addressErrorLabel.Content = "Address is required.";
            }
            else
            {
                addressErrorLabel.Content = "";
            }

            return isNewAddressEmpty;
        }

        public (bool, bool) ValidateNipTextField(TextBox nipTextField, Label nipErrorLabel)
        {
            bool isNipEmpty = false;
            bool isNipError = false;

            if (nipTextField.Text.Length == 0)
                isNipEmpty = true;
            else
            {
                if (!new Regex(".{1,255}").IsMatch(nipTextField.Text))
                    isNipError = true;
            }

            if (isNipEmpty)
            {
                nipErrorLabel.Visibility = Visibility.Visible;
                nipErrorLabel.Content = "NIP is required.";
            }
            else
            {
                if (isNipError)
                {
                    nipErrorLabel.Visibility = Visibility.Visible;
                    nipErrorLabel.Content = "Required length is from 1 to 255 characters.";
                }
                else
                    nipErrorLabel.Content = "";
            }

            return (isNipEmpty, isNipError);
        }
    }
}

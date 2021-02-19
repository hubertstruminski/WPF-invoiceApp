using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_invoiceApp.context;

namespace WPF_invoiceApp.template.details
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerDetailsWindow.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : UserControl
    {
        private DatabaseContext context;

        public CustomerDetailsWindow(DatabaseContext context)
        {
            InitializeComponent();
            this.context = context;
        }

        public CustomerDetailsWindow(Customer selectedItem, DatabaseContext context) : this(context)
        {
            nameLabel.Text = selectedItem.Name;
            emailValueLabel.Text = selectedItem.Email;
            addressValueLabel.Text = selectedItem.Address.AddressName;
            phoneNumberValueLabel.Text = selectedItem.PhoneNumber;
            countryValueLabel.Text = selectedItem.Address.Country;
            nipValueLabel.Text = selectedItem.Nip;
            commentValueLabel.Text = selectedItem.Note;
            websiteValueLabel.Text = selectedItem.Website;

            scrollViewer.Children.Clear();

            foreach(Invoice invoice in selectedItem.Invoices)
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

                TextBlock invoiceName = new TextBlock();
                invoiceName.Text = "Invoice";
                invoiceName.FontWeight = FontWeights.Bold;
                s1c1.Children.Add(invoiceName);

                TextBlock number = new TextBlock();
                number.Text = "Number: " + invoice.Number;
                s1c1.Children.Add(number);

                TextBlock date = new TextBlock();
                date.Text = "Date: " + invoice.Date;
                s1c1.Children.Add(date);

                TextBlock deadline = new TextBlock();
                deadline.Text = "Deadline: " + invoice.Deadline;
                s1c1.Children.Add(deadline);

                Grid.SetColumn(s1c1, 0);

                // ---------------------------

                StackPanel s2c2 = new StackPanel();
                s2c2.Orientation = Orientation.Vertical;

                TextBlock customerName = new TextBlock();
                customerName.Text = "Customer";
                customerName.FontWeight = FontWeights.Bold;
                s2c2.Children.Add(customerName);

                TextBlock name = new TextBlock();
                name.Text = "Name: " + invoice.Customer.Name;
                s2c2.Children.Add(name);

                TextBlock address = new TextBlock();
                address.Text = "Address: " + invoice.Customer.Address.AddressName + " " + invoice.Customer.Address.Country;
                s2c2.Children.Add(address);

                TextBlock phoneNumber = new TextBlock();
                phoneNumber.Text = "Phone number: " + invoice.Customer.PhoneNumber;
                s2c2.Children.Add(phoneNumber);

                Grid.SetColumn(s2c2, 1);

                // -------------------

                StackPanel s3c3 = new StackPanel();
                s3c3.Orientation = Orientation.Vertical;

                TextBlock status = new TextBlock();
                status.Text = "Status";
                status.FontWeight = FontWeights.Bold;
                s3c3.Children.Add(status);

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

                scrollViewer.Children.Add(grid);
            }
        }
    }
}
